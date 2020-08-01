using System;
using UnityEngine;

namespace SparuvianConnection.Adoptatron
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


    }
}