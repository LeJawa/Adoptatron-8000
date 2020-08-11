using SparuvianConnection.Adoptatron.Gameplay.Skills;

namespace SparuvianConnection.Adoptatron.Gameplay.Marbles
{
    public class PatienceMarble : SkillMarble
    {
        protected override void Start()
        {
            base.Start();
            Skill = new Patience(1);
        }
    }
}