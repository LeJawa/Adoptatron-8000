using SparuvianConnection.Adoptatron.Gameplay.Skills;

namespace SparuvianConnection.Adoptatron.Gameplay.Marbles
{
    public class SitMarble : SkillMarble
    {
        protected override void Start()
        {
            base.Start();
            Skill = new Sit(1);
        }
    }
}