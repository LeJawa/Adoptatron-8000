using System;
using SparuvianConnection.Adoptatron.Gameplay.Marbles;

namespace SparuvianConnection.Adoptatron.Gameplay
{
    public class GameEvents
    {
        #region Singleton pattern
        private static GameEvents _current;
        public static GameEvents Instance
        {
            get
            {
                if (_current == null)
                {
                    _current = new GameEvents();
                }
                return _current;
            }
        }
        #endregion

        #region Action<Marble> OnMarbleCollision
        public event Action<Marble> OnMarbleCollision;

        public void TriggerMarbleCollisionEvent(Marble marble)
        {
            OnMarbleCollision?.Invoke(marble);
        }
        #endregion
        
        #region Action OnNewPlayerShot
        public event Action OnNewPlayerShot;

        public void TriggerNewPlayerShotEvent()
        {
            OnNewPlayerShot?.Invoke();
        }
        #endregion
        
        #region Action<int> OnLoadLevel
        public event Action<int> OnLoadLevel;

        public void TriggerLoadLevelEvent(int level)
        {
            OnLoadLevel?.Invoke(level);
        }
        #endregion
        
        #region Action<int> OnLoadNextLevel
        public event Action OnLoadNextLevel;

        public void TriggerLoadNextLevelEvent()
        {
            OnLoadNextLevel?.Invoke();
        }
        #endregion

    }
}