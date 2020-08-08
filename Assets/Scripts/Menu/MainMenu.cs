using System;
using System.Collections;
using SparuvianConnection.Adoptatron.Audio;
using SparuvianConnection.Adoptatron.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SparuvianConnection.Adoptatron.Menu
{
    public class MainMenu : MonoBehaviour
    {
        private void Start()
        {
            foreach (var dogData in FindObjectsOfType<DogData>())
            {
                Destroy(dogData.gameObject);
            }

            
        }

        public void StartGame()
        {
            StartCoroutine(StartGameCoroutine());
        }

        private IEnumerator StartGameCoroutine()
        {
            AnimationManager.Instance.StartEndSceneAnimation();
            yield return new WaitForSeconds(1.5f);
            
            SceneManager.LoadScene("Scenes/Scene1");
        }
        
        public void GoToInfoMenu()
        {
            AudioManager.Play(AudioClipName.Select);
            StartCoroutine(GoToInfoMenuCoroutine());
        }

        private IEnumerator GoToInfoMenuCoroutine()
        {
            AnimationManager.Instance.StartEndSceneAnimation();
            yield return new WaitForSeconds(1.5f);
            
            SceneManager.LoadScene("Scenes/InfoMenu");
        }
    }
}