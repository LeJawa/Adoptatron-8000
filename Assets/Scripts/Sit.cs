namespace SparuvianConnection.Adoptatron
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