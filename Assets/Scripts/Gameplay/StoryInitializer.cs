using UnityEngine;

namespace SparuvianConnection.Adoptatron.Gameplay
{
    public class StoryInitializer : MonoBehaviour
    {
        private void Awake()
        {
            StoryManager.Instance.Initialize();
            
            Destroy(gameObject);
        }
    }
}