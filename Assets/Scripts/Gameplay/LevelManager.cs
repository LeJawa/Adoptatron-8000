using System;
using System.Collections;
using SparuvianConnection.Adoptatron.Gameplay.Marbles;
using SparuvianConnection.Adoptatron.Gameplay.Skills;
using SparuvianConnection.Adoptatron.GUI;
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
        
        private HUD _hud;

        private PlayerController _playerController;
        
        public int Level { get; private set; }


        public LevelManager(int level)
        {
            InitializeFields(level);

            SubscribeToEvents();

            ResetCombo();
        }

        private void InitializeFields(int level)
        {
            Level = level;
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

        }
        
        private IEnumerator LoadLevelCoroutine(int level)
        {
            // Start loading the scene
            AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync("Level" + level, LoadSceneMode.Single);
            // Wait until the level finish loading
            while (!asyncLoadLevel.isDone)
                yield return null;
            // Wait a frame so every Awake and Start method is called
            yield return new WaitForEndOfFrame();
            
            InitializeFields(level);
            ResetCombo();
        }
        

        private void HandleNewPlayerShotEvent()
        {
            _currentNumberOfTries++;
            ResetCombo();

            if (_currentNumberOfTries >= TotalNumberOfTries)
            {
                _playerController.CannotShootAnymore();
            }
        }

        private void ResetCombo()
        {
            _currentCombo = StartingCombo;
            _numberOfCollisionsInThisRound = 0;
            
            UpdateComboHUD();
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
            _hud.ChangeSitSkillMasteryTo(GameManager.Instance.CurrentDog.GetMasteryOfSkill(SkillName.Sit));
        }
        
        private void UpdateComeSkillHUD()
        {
            _hud.ChangeComeSkillMasteryTo(GameManager.Instance.CurrentDog.GetMasteryOfSkill(SkillName.Come));
        }

        private void UpdateComboHUD()
        {
            _hud.ChangeComboTo(_currentCombo);
        }
    }
}