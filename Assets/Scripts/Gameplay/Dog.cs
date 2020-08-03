using System.Collections.Generic;
using SparuvianConnection.Adoptatron.Gameplay.Skills;

namespace SparuvianConnection.Adoptatron.Gameplay
{
    public class Dog
    {
        private Dictionary<SkillName, Skill> _dictionaryOfSkills;

        public Dog()
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

            if (MasteryIsEnoughToReachPowerUp(skill.Name))
            {
                GameEvents.Instance.TriggerNewSkillPowerUpAvailableEvent(skill.Name);
            }
        }

        private bool MasteryIsEnoughToReachPowerUp(SkillName skillName)
        {
            return true;
        }

        public int GetMasteryOfSkill(SkillName skill)
        {
            return _dictionaryOfSkills[skill].Mastery;
        }
        
    }
}