namespace SparuvianConnection.Adoptatron.Gameplay.Skills
{
    public class Come : Skill
    {
        public Come() : base(){}
        
        public Come(int startingMastery) : base(startingMastery) {}
        protected override void SetSkillName()
        {
            Name = SkillName.Come;
        }
    }
}