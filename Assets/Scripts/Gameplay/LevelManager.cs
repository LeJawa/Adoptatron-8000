using System;
using System.Collections;
using SparuvianConnection.Adoptatron.Gameplay.Marbles;
using SparuvianConnection.Adoptatron.Gameplay.Skills;
using SparuvianConnection.Adoptatron.GUI;
using SparuvianConnection.Adoptatron.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SparuvianConnection.Adoptatron.Gameplay
{
    public class LevelManager
    {
        private int _currentCombo;
        private const int StartingCombo = 1;
        private int _numberOfCollisionsInThisRound = 0;

        private const int TotalNumberOfTries = 5;
        private int _currentNumberOfTries = 0;

        private Dog _currentDog;
        
        private HUD _hud;

        private PlayerController _playerController;
        
        public int Level { get; private set; }


        public LevelManager(int level, Dog currentDog)
        {
            Level = level;
            _currentDog = currentDog;
            
            InitializeFields();

            SubscribeToEvents();

            ResetCombo();
            UpdateAllHUDs();
        }

        private void InitializeFields()
        {
            _hud = GameObject.FindWithTag("HUD").GetComponent<HUD>();
            _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        private void SubscribeToEvents()
        {
            GameEvents.Instance.OnMarbleCollision += HandleMarbleCollisionEvent;
            GameEvents.Instance.OnNewPlayerShot += HandleNewPlayerShotEvent;
        }

        public void LoadLevel(int level)
        {
            // SceneManager.LoadScene("Level" + level);
            //
            // InitializeFields(level);
            // ResetCombo();
            
            Debug.Log("Loading level " + level);
            CoroutineHelper.Instance.StartCoroutine(LoadLevelCoroutine(level));
        }
        
        public void LoadNextLevel()
        {
            LoadLevel(Level + 1);
        }
        
        private IEnumerator LoadLevelCoroutine(int level)
        {
            GameManager.Instance.StartEndSceneAnimation();
            yield return new WaitForSeconds(1.5f);
            
            yield return SceneManager.LoadSceneAsync("Level" + level, LoadSceneMode.Single);
            // Wait a frame so every Awake and Start method is called
            yield return new WaitForEndOfFrame();
            
            // string currentScene = SceneManager.GetActiveScene().name;
            //
            // SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level" + level));
            // SceneManager.UnloadSceneAsync(currentScene);

            Level = level;
            InitializeFields();
            ResetCombo();
            
            UpdateAllHUDs();
        }
        

        private void HandleNewPlayerShotEvent()
        {
            _currentNumberOfTries++;
            ResetCombo();
            UpdateComboHUD();
            
            if (_currentNumberOfTries >= TotalNumberOfTries)
            {
                _playerController.CannotShootAnymore();
            }
        }

        private void ResetCombo()
        {
            _currentCombo = StartingCombo;
            _numberOfCollisionsInThisRound = 0;
        }
        
        private void HandleMarbleCollisionEvent(Marble marble)
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
            switch (skillName)
            {
                case SkillName.Sit:
                    UpdateSitSkillHUD();
                    break;
                case SkillName.Come:
                    UpdateComeSkillHUD();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(skillName), skillName, null);
            }
        }

        private void UpdateSitSkillHUD()
        {
            _hud.ChangeSitSkillMasteryTo(_currentDog.GetMasteryOfSkill(SkillName.Sit));
        }
        
        private void UpdateComeSkillHUD()
        {
            _hud.ChangeComeSkillMasteryTo(_currentDog.GetMasteryOfSkill(SkillName.Come));
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

    }
}