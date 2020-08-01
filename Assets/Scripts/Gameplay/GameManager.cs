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
            _currentDog = GameObject.FindWithTag("Player").GetComponent<Dog>();
            _hud = GameObject.FindWithTag("HUD").GetComponent<HUD>();
            
            _levelManager = new LevelManager(1);

            GameEvents.Instance.OnLoadNewLevel += HandleLoadNewLevelEvent;
        }

        private void HandleLoadNewLevelEvent(int level)
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