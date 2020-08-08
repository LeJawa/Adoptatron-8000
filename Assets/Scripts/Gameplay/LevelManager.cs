using System;
using System.Collections;
using SparuvianConnection.Adoptatron.Gameplay.Marbles;
using SparuvianConnection.Adoptatron.Gameplay.Skills;
using SparuvianConnection.Adoptatron.GUI;
using SparuvianConnection.Adoptatron.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
// ReSharper disable AccessToStaticMemberViaDerivedType

namespace SparuvianConnection.Adoptatron.Gameplay
{
    public class LevelManager
    {
        private int _currentCombo;
        private const int StartingCombo = 1;
        private int _numberOfCollisionsInThisRound = 0;

        private const int TotalNumberOfTries = 1000;
        private int _currentNumberOfTries = 0;

        private Dog _currentDog;
        
        private HUD _hud;

        private PlayerMarble _playerMarble;
        
        private RewindManager _rewindManager;

        private DateTime _levelTimer = new DateTime();

        public int Level { get; private set; }


        public LevelManager(int level, Dog currentDog)
        {
            Level = level;
            _currentDog = currentDog;
            _currentNumberOfTries = 0;
            
            FindGameObjectsInScene();

            SubscribeToEvents();

            ResetCombo();
            UpdateAllHUDs();
        }

        private void FindGameObjectsInScene()
        {
            _hud = GameObject.FindWithTag("HUD").GetComponent<HUD>();
            _playerMarble = GameObject.FindWithTag("Player").GetComponent<PlayerMarble>();
            _rewindManager = GameObject.FindWithTag("Rewind").GetComponent<RewindManager>();
        }

        private void SubscribeToEvents()
        {
            GameEvents.Instance.OnMarbleCollision += HandleMarbleCollisionEvent;
            GameEvents.Instance.OnNewPlayerShot += HandleNewPlayerShotEvent;

            GameEvents.Instance.OnSkillPowerUpActivated += HandleSkillPowerUpActivated;
            GameEvents.Instance.OnRewindStart += RewindLevel;
        }

        public void RewindLevel()
        {
            _currentDog.AddRewindCount();
            _rewindManager.StartRewind();
        }

        public void LoadLevel(int level)
        {
            Debug.Log("Loading level " + level);
            
            GameEvents.Instance.TriggerAllMarblesStopEvent();

            CalculateTimeSpentInLevel();


            CoroutineHelper.Instance.StartCoroutine(LoadLevelCoroutine(level));
        }

        private void CalculateTimeSpentInLevel()
        {
            var timeNow = DateTime.Now;

            var timeSpan = timeNow.Subtract(_levelTimer);

            _currentDog.AddToTotalTime((float) timeSpan.TotalSeconds);
        }

        public void LoadNextLevel()
        {
            LoadLevel(Level + 1);
        }

        public void LoadLastLevel()
        {
            LoadLevel(Level - 1);
        }

        private IEnumerator LoadLevelCoroutine(int level)
        {
            AnimationManager.Instance.StartEndSceneAnimation();
            yield return new WaitForSeconds(1.5f);
            
            yield return SceneManager.LoadSceneAsync("Level" + level, LoadSceneMode.Single);
            // Wait a frame so every Awake and Start method is called
            yield return new WaitForEndOfFrame();

            Level = level;
            _currentNumberOfTries = 0;
            FindGameObjectsInScene();
            GameManager.Instance.FindGameObjectsInScene();
            ResetCombo();
            
            UpdateAllHUDs();
        }


        private void HandleNewPlayerShotEvent()
        {
            _rewindManager.StartRecording();
            
            _currentNumberOfTries++;
            ResetCombo();
            UpdateComboHUD();
            
            if (_currentNumberOfTries >= TotalNumberOfTries)
            {
                _playerMarble.CannotShootAnymore();
            }

            _levelTimer = DateTime.Now;
        }

        private void ResetCombo()
        {
            _currentCombo = StartingCombo;
            _numberOfCollisionsInThisRound = 0;
        }

        private void HandleMarbleCollisionEvent(SkillMarble marble)
        {
            Debug.Log("Skill " + marble.Skill.Name.ToString() + " with mastery " + marble.Skill.Mastery);

            _numberOfCollisionsInThisRound++;
            CalculateAndUpdateCombo();
            
            GameManager.Instance.CurrentDog.UpdateSkill(marble.Skill, _currentCombo);
            UpdateHUDOfSkill(marble.Skill.Name);
        }

        private void CalculateAndUpdateCombo()
        {
            if (_numberOfCollisionsInThisRound < 2)
            {
                _currentCombo = 1;
            } 
            else if (_numberOfCollisionsInThisRound < 5)
            {
                _currentCombo = 2;
            } 
            else if (_numberOfCollisionsInThisRound < 10)
            {
                _currentCombo = 3;
            } 
            else if (_numberOfCollisionsInThisRound < 20)
            {
                _currentCombo = 4;
            } 
            else // _numberOfCollisionsInThisRound > 20
            {
                _currentCombo = 5;
            }
            
            UpdateComboHUD();
        }

        private void UpdateHUDOfSkill(SkillName skillName)
        {
            _hud.ChangeSkillMasteryTo(skillName, _currentDog.GetMasteryOfSkill(skillName));
        }

        private void UpdateSitSkillHUD()
        {
            UpdateHUDOfSkill(SkillName.Sit);
        }

        private void UpdateComeSkillHUD()
        {
            UpdateHUDOfSkill(SkillName.Come);
        }

        private void UpdateComboHUD()
        {
            _hud.ChangeComboTo(_currentCombo);
        }

        private void UpdateAllHUDs()
        {
            UpdateSitSkillHUD();
            UpdateComeSkillHUD();
            UpdateComboHUD();
        }

        private void HandleSkillPowerUpActivated(SkillName skillName)
        {
            if (skillName == SkillName.Sit)
            {
                GameEvents.Instance.TriggerAllMarblesStopEvent();
            }
        }

        public void EndGame()
        {
            CalculateTimeSpentInLevel();
            
            CoroutineHelper.Instance.StartCoroutine(EndGameCoroutine());
        }

        private IEnumerator EndGameCoroutine()
        {
            GameObject dogDataGO = GameObject.Instantiate(new GameObject("DogData"));
            GameObject.DontDestroyOnLoad(dogDataGO);
            
            DogData dogData = dogDataGO.AddComponent<DogData>();
            dogData.Dog = _currentDog;
            
            AnimationManager.Instance.StartEndSceneAnimation();
            yield return new WaitForSeconds(1.5f);

            SceneManager.LoadScene("Scenes/EndDialogue");


        }
    }
}