namespace SparuvianConnection.Adoptatron.Gameplay.Skills
{
    public class Patience : Skill
    {
        public Patience() : base(){}

        public Patience(int startingMastery) : base(startingMastery) {}

        protected override void SetSkillName()
        {
            Name = SkillName.Patience;
        }
    }
}