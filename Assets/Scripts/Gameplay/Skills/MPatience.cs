namespace SparuvianConnection.Adoptatron.Gameplay.Skills
{
    public class MPatience : Skill
    {
        public MPatience() : base(){}

        public MPatience(int startingMastery) : base(startingMastery) {}

        protected override void SetSkillName()
        {
            Name = SkillName.MPatience;
        }
    }
}