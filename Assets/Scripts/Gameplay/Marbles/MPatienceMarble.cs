using SparuvianConnection.Adoptatron.Gameplay.Skills;

namespace SparuvianConnection.Adoptatron.Gameplay.Marbles
{
    public class MPatienceMarble : SkillMarble
    {
        protected override void Start()
        {
            base.Start();
            Skill = new MPatience(1);
        }
    }
}