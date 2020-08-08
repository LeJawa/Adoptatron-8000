using System;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Utils
{
    public class Singleton : MonoBehaviour
    {
        private void Awake()
        {
            if (GameObject.FindObjectsOfType<Singleton>().Length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
            
        }
    }
}