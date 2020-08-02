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

        private Dog _currentDog;

        private HUD _hud;


        private GameManager()
        {
            _currentDog = GameObject.FindWithTag("Player").GetComponent<Dog>();
            _hud = GameObject.FindWithTag("HUD").GetComponent<HUD>();

            GameEvents.Instance.OnMarbleCollision += HandleMarbleCollision;

        }

        private void HandleMarbleCollision(Marble marble)
        {
            _currentDog.UpdateSkill(marble.Skill);
            Debug.Log("Skill " + marble.Skill.Name.ToString() + " with mastery " + marble.Skill.Mastery);

            UpdateHUDOfSkill(marble.Skill.Name);
        }

        private void UpdateHUDOfSkill(SkillName skillName)
        {
            switch (skillName)
            {
                case SkillName.Sit:
                    UpdateSitSkillText();
                    break;
                case SkillName.Come:
                    UpdateComeSkillText();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(skillName), skillName, null);
            }
        }

        private void UpdateSitSkillText()
        {
            _hud.ChangeSitSkillMasteryTo(_currentDog.GetMasteryOfSkill(SkillName.Sit));
        }
        
        private void UpdateComeSkillText()
        {
            _hud.ChangeComeSkillMasteryTo(_currentDog.GetMasteryOfSkill(SkillName.Come));
        }


        public void Initialize()
        {
            if (_initialized) return;

            _initialized = true;
        }
        
    }
}