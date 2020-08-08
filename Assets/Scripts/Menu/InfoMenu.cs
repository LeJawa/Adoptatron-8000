using System.Collections;
using SparuvianConnection.Adoptatron.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SparuvianConnection.Adoptatron.Menu
{
    public class InfoMenu : MonoBehaviour
    {

        public void BackToMenu()
        {
            AudioManager.Play(AudioClipName.Select);
            StartCoroutine(BackToMenuCoroutine());
        }

        private IEnumerator BackToMenuCoroutine()
        {
            AnimationManager.Instance.StartEndSceneAnimation();
            yield return new WaitForSeconds(1.5f);
            
            SceneManager.LoadScene("Scenes/MainMenu");
        }
        
    }
}