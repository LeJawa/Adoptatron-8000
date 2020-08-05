using System;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Gameplay
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class Gate : MonoBehaviour
    {
        public Effector2D effector;
        
        public Transform exitDirection;
        public float exitForce = 3f;
        private static readonly int GoingThroughGate = Animator.StringToHash("goingThroughGate");


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Wall"))
                {
                    wall.layer = 11; // DefaultNoPlayer
                }

                effector.enabled = false;
                
                other.GetComponent<Animator>().SetTrigger(GoingThroughGate);

                Rigidbody2D rb2d = other.GetComponent<Rigidbody2D>();
                rb2d.AddForce(exitDirection.right * exitForce, ForceMode2D.Impulse);
                other.transform.right = rb2d.velocity;
                
                
                
                TriggerAppropriateEvent();
            }
        }

        protected abstract void TriggerAppropriateEvent();
    }
}