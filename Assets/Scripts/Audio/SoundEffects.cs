using UnityEngine;

namespace SparuvianConnection.Adoptatron.Audio
{
    public class SoundEffects : MonoBehaviour
    {
        private void Start()
        {
            AudioSource source = GetComponent<AudioSource>();
            
            AudioManager.Initialize(source);
            
        }
    }
}