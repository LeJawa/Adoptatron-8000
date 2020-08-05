using System;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Dialogue
{
    [CreateAssetMenu(menuName = "New dialogue")]
    public class DialogueScriptableObject : ScriptableObject
    {
        
        [TextArea]
        public string[] dialogueLinesArray;

        [NonSerialized]
        private int _currentIndex = 0;

        private void Awake()
        {
            _currentIndex = 0;
        }

        public string GetNextLine()
        {
            if (_currentIndex >= dialogueLinesArray.Length) return null;
            
            _currentIndex++;
            return dialogueLinesArray[_currentIndex - 1];
        }

        public string SeeNextLine()
        {
            return _currentIndex < dialogueLinesArray.Length ? dialogueLinesArray[_currentIndex] : null;
        }
        

    }
}