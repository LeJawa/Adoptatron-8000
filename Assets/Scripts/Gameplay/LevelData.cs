using UnityEngine;
using UnityEngine.SceneManagement;

namespace SparuvianConnection.Adoptatron.Gameplay
{
    [CreateAssetMenu(fileName = "level", menuName = "New LevelData", order = 0)]
    public class LevelData : ScriptableObject
    {
        public int numberOfShots;
    }
}