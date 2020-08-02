using SparuvianConnection.Adoptatron.GUI;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Gameplay
{
    public class GameManager
    {
        #region Singleton pattern

        private static GameManager _current;
        public static GameManager Instance
        {
            get
            {
                if (_current == null)
                {
                    _current = new GameManager();
                }
                return _current;
            }
        }
        #endregion

        private bool _initialized = false;

        private readonly Dog _currentDog;

        private readonly HUD _hud;

        private LevelManager _levelManager;
        public Dog CurrentDog => _currentDog;

        private GameManager()
        {
            _currentDog = new Dog();
            _hud = GameObject.FindWithTag("HUD").GetComponent<HUD>();
            
            _levelManager = new LevelManager(1, _currentDog);

            GameEvents.Instance.OnLoadLevel += HandleLoadLevelEvent;
            GameEvents.Instance.OnLoadNextLevel += HandleLoadNextLevelEvent;
        }

        private void HandleLoadNextLevelEvent()
        {
            _levelManager.LoadNextLevel();
        }

        private void HandleLoadLevelEvent(int level)
        {
            _levelManager.LoadLevel(level);
        }

        public void Initialize()
        {
            if (_initialized) return;

            _initialized = true;
        }
        

        

        
        
    }
}