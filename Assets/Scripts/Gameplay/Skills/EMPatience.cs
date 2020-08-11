namespace SparuvianConnection.Adoptatron.Gameplay.Skills
{
    public class EMPatience : Skill
    {
        public EMPatience() : base(){}

        public EMPatience(int startingMastery) : base(startingMastery) {}

        protected override void SetSkillName()
        {
            Name = SkillName.EMPatience;
        }
    }
}