using System;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Gameplay.Marbles
{
    public abstract class Marble : MonoBehaviour
    {
        protected Rigidbody2D _rb2d;

        protected virtual void Start()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            
            GameEvents.Instance.OnAllMarblesStop += StopMarbleMovement;
        }

        protected void StopMarbleMovement()
        {
            _rb2d.velocity = Vector2.zero;
        }

        protected void OnDestroy()
        {
            GameEvents.Instance.OnAllMarblesStop -= StopMarbleMovement;
        }
    }
}