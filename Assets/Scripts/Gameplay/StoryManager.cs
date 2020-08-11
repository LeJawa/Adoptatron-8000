using System.Collections;
using SparuvianConnection.Adoptatron.Audio;
using SparuvianConnection.Adoptatron.Dialogue;
using SparuvianConnection.Adoptatron.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SparuvianConnection.Adoptatron.Gameplay
{
    public class StoryManager
    {
        #region Singleton pattern

        private static StoryManager _current;
        public static StoryManager Instance
        {
            get
            {
                if (_current == null)
                {
                    _current = new StoryManager();
                }
                return _current;
            }
        }
        #endregion

        private bool _initialized = false;


        private StoryManager()
        {
            GameEvents.Instance.OnDialogueEndSetAction += HandleDialogueEndSetActionEvent;
        }
        
        private void HandleDialogueEndSetActionEvent(DialogueEndAction action)
        {
            switch (action)
            {
                case DialogueEndAction.StartGameplay:
                    StartGameplay();
                    break;
                case DialogueEndAction.EndGame:
                    EndGame();
                    break;
            }
        }

        private void EndGame()
        {
            GoToInfoMenu();
        }
        
        public void GoToInfoMenu()
        {
            AudioManager.Play(AudioClipName.Select);
            CoroutineHelper.Instance.StartCoroutine(GoToInfoMenuCoroutine());
        }

        private IEnumerator GoToInfoMenuCoroutine()
        {
            yield return new WaitForSeconds(2f);
            AnimationManager.Instance.StartEndSceneAnimation();
            yield return new WaitForSeconds(1.5f);
            
            SceneManager.LoadScene("Scenes/InfoMenu");
        }

        private void StartGameplay()
        {
            GameManager.Instance.StartGameplay();
            // CoroutineHelper.Instance.StartCoroutine(StartGameplayCoroutine());
        }

        private IEnumerator StartGameplayCoroutine()
        {
            yield return new WaitForSeconds(1);
            
            AnimationManager.Instance.StartEndSceneAnimation();
            yield return new WaitForSeconds(1.5f);
            
            SceneManager.LoadScene("Scenes/Levels/Level1");
        }


        public void Initialize()
        {
            if (_initialized) return;

            _initialized = true;
        }
        
        
        
        
        
        
    }
}