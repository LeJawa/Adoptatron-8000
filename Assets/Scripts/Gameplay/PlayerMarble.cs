using SparuvianConnection.Adoptatron.Gameplay.Marbles;
using SparuvianConnection.Adoptatron.Utils;
using Unity.Mathematics;
using UnityEngine;

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
