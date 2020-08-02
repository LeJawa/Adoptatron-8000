using SparuvianConnection.Adoptatron.Utils;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Gameplay
{
    public class GameInitializer : MonoBehaviour
    {
        private void Start()
        {
            ScreenUtils.Initialize();
            GameManager.Instance.Initialize();
            
            Destroy(gameObject);
        }
    }
}