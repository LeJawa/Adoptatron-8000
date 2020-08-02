using SparuvianConnection.Adoptatron.Gameplay.Skills;

namespace SparuvianConnection.Adoptatron.Gameplay.Marbles
{
    public class SitMarble : Marble
    {
        private void Start()
        {
            Skill = new Sit(1);
        }
    }
}