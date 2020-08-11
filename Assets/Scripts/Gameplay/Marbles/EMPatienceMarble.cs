using SparuvianConnection.Adoptatron.Gameplay.Skills;

namespace SparuvianConnection.Adoptatron.Gameplay.Marbles
{
    public class EMPatienceMarble : SkillMarble
    {
        protected override void Start()
        {
            base.Start();
            Skill = new EMPatience(1);
        }
    }
}