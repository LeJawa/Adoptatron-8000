namespace SparuvianConnection.Adoptatron.Gameplay.Skills
{
    public abstract class Skill
    {
        public SkillName Name { get; protected set; }
        
        public int Mastery { get; protected set; }
        
        protected Skill()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            SetSkillName();
        }

        protected Skill(int startingMastery) : this()
        {
            Mastery = startingMastery;
        }

        protected abstract void SetSkillName();

        public void IncreaseMasteryByAmount(int amount)
        {
            Mastery += amount;
        }
    }
}