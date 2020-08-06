using System;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Dialogue
{
    [CreateAssetMenu(menuName = "New dialogue")]
    public class DialogueScriptableObject : ScriptableObject
    {
        
        [NonSerialized]
        public string[] _dialogueLinesArray;

        [TextArea(20, 50)] public string dialogueText;

        [NonSerialized]
        private int _currentIndex = 0;

        public void Initialize()
        {
            ParseDialogueText();
            _currentIndex = 0;
        }

        private void ParseDialogueText()
        {
            if (_dialogueLinesArray != null) return;
            
            _dialogueLinesArray = dialogueText.Split(new string[] {Environment.NewLine, "\n"},
                StringSplitOptions.RemoveEmptyEntries);

        }

        public string GetNextLine()
        {
            if (_dialogueLinesArray == null)
            {
                Initialize();
            }
            
            if (_currentIndex >= _dialogueLinesArray.Length) return null;
            
            _currentIndex++;
            return _dialogueLinesArray[_currentIndex - 1];
        }

        public string SeeNextLine()
        {
            return _currentIndex < _dialogueLinesArray.Length ? _dialogueLinesArray[_currentIndex] : null;
        }
        

    }
}