using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SparuvianConnection.Adoptatron
{
    public class GameManager
    {
        #region Singleton pattern

        private static GameManager _current;
        public static GameManager Instance
        {
            get
            {
                if (_current == null)
                {
                    _current = new GameManager();
                }
                return _current;
            }
        }
        #endregion

        private bool _initialized = false;

        private readonly Dog _currentDog;

        private readonly HUD _hud;

        private int _currentCombo;
        private const int StartingCombo = 1;
        private int _numberOfCollisionsInThisRound = 0;

        private GameManager()
        {
            _currentDog = GameObject.FindWithTag("Player").GetComponent<Dog>();
            _hud = GameObject.FindWithTag("HUD").GetComponent<HUD>();

            GameEvents.Instance.OnMarbleCollision += HandleMarbleCollisionEvent;
            GameEvents.Instance.OnResetCombo += HandleResetComboEvent;
            
            ResetCombo();
        }

        private void HandleResetComboEvent()
        {
            ResetCombo();
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
            
            _currentDog.UpdateSkill(marble.Skill, _currentCombo);
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


        public void Initialize()
        {
            if (_initialized) return;

            _initialized = true;
        }
        
    }
}