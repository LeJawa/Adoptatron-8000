using SparuvianConnection.Adoptatron.Gameplay.Skills;

namespace SparuvianConnection.Adoptatron.Gameplay.Marbles
{
    public class ComeMarble : Marble
    {
        private void Start()
        {
            Skill = new Come(1);
        }
    }
}