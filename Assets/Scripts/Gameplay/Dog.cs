using System.Collections.Generic;
using SparuvianConnection.Adoptatron.Gameplay.Skills;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Gameplay
{
    public class Dog : MonoBehaviour
    {
        private Dictionary<SkillName, Skill> _dictionaryOfSkills;

        private void Start()
        {
            _dictionaryOfSkills = new Dictionary<SkillName, Skill>();

            PopulateDictionaryOfSkills();
        }

        private void PopulateDictionaryOfSkills()
        {
            _dictionaryOfSkills.Add(SkillName.Sit, new Sit());
            _dictionaryOfSkills.Add(SkillName.Come, new Come());
        }

        public void UpdateSkill(Skill skill, int comboMultiplier)
        {
            _dictionaryOfSkills[skill.Name].IncreaseMasteryByAmount(skill.Mastery * comboMultiplier);
        }

        public int GetMasteryOfSkill(SkillName skill)
        {
            return _dictionaryOfSkills[skill].Mastery;
        }
        
    }
}