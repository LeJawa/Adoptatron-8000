using System.Collections.Generic;
using SparuvianConnection.Adoptatron.Gameplay.Skills;

namespace SparuvianConnection.Adoptatron.Gameplay
{
    public class Dog
    {
        private Dictionary<SkillName, Skill> _dictionaryOfSkills;

        public int RewindCount { get; private set; }
        public float TotalTime { get; private set; }

        public Dog()
        {
            _dictionaryOfSkills = new Dictionary<SkillName, Skill>();

            PopulateDictionaryOfSkills();
        }

        private void PopulateDictionaryOfSkills()
        {
            _dictionaryOfSkills.Add(SkillName.Patience, new Patience());
            _dictionaryOfSkills.Add(SkillName.MPatience, new MPatience());
            _dictionaryOfSkills.Add(SkillName.EMPatience, new EMPatience());
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
            if (_dictionaryOfSkills[skillName].Mastery != 0 && _dictionaryOfSkills[skillName].Mastery%10 == 0)
            {
                return true;
            }
            return false;
        }

        public int GetMasteryOfSkill(SkillName skill)
        {
            return _dictionaryOfSkills[skill].Mastery;
        }

        public void AddRewindCount()
        {
            RewindCount++;
        }

        public void AddToTotalTime(float time)
        {
            TotalTime += time;
        }
        
    }
}