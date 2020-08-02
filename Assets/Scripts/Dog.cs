using System.Collections.Generic;
using UnityEngine;

namespace SparuvianConnection.Adoptatron
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

        public void UpdateSkill(Skill skill)
        {
            _dictionaryOfSkills[skill.Name].IncreaseMasteryByAmount(skill.Mastery);
        }

        public int GetMasteryOfSkill(SkillName skill)
        {
            return _dictionaryOfSkills[skill].Mastery;
        }
        
    }
}