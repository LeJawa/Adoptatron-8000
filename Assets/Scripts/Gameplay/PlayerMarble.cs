using SparuvianConnection.Adoptatron.Gameplay.Marbles;

namespace SparuvianConnection.Adoptatron.Gameplay
{
    public class PlayerMarble : Marble
    {
        public bool CanShoot { get; private set; } = true;

        public void CannotShootAnymore()
        {
            CanShoot = false;
        }
        
        public void CanShootAgain()
        {
            CanShoot = true;
        }
    }
}
