﻿using SparuvianConnection.Adoptatron.Gameplay.Skills;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Gameplay.Marbles
{
    public abstract class Marble : MonoBehaviour
    {
        public Skill Skill { get; protected set; }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Wall")) return;
            
            GameEvents.Instance.TriggerMarbleCollisionEvent(this);
        }
    }
}