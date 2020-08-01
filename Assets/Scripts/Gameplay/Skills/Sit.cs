namespace SparuvianConnection.Adoptatron.Gameplay.Skills
{
    public class Sit : Skill
    {
        public Sit() : base(){}

        public Sit(int startingMastery) : base(startingMastery) {}

        protected override void SetSkillName()
        {
            Name = SkillName.Sit;
        }
    }
}