using SparuvianConnection.Adoptatron.Gameplay.Skills;

namespace SparuvianConnection.Adoptatron.Gameplay.Marbles
{
    public class ComeMarble : SkillMarble
    {
        protected override void Start()
        {
            base.Start();
            Skill = new Come(1);
        }
    }
}