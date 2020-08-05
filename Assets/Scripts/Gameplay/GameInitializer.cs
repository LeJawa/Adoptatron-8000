using SparuvianConnection.Adoptatron.Utils;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Gameplay
{
    public class GameInitializer : MonoBehaviour
    {
        public GameObject prefabCursor;
        
        
        private void Awake()
        {
            Instantiate(prefabCursor);
            
            ScreenUtils.Initialize();
            GameManager.Instance.Initialize();
            
            Destroy(gameObject);
        }
    }
}