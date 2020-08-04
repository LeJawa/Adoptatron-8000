using System;
using SparuvianConnection.Adoptatron.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace SparuvianConnection.Adoptatron.Dialogue
{
    [CreateAssetMenu(menuName = "New dialogue node")]
    public class DialogueNode : ScriptableObject
    {
        public DialogueScriptableObject _dialogue;
        public UnityEvent OnDialogueEnd;

        public AnswerDialogueDictionary answersDictionary;

        [NonSerialized]
        private string nextLine;

        [NonSerialized] private bool _endOfDialogue = false;

        private void Awake()
        {
            nextLine = _dialogue.GetNextLine();
        }

        public string GetNextLine()
        {
            if (nextLine == null)
            {
                return null;
            }
            
            string message = nextLine;
            nextLine = _dialogue.GetNextLine();

            if (nextLine != null)
            {
                return message;
            }
            else // Last line
            {
                ShowAnswers();
                return message;
            }
        }

        private void ShowAnswers()
        {
            foreach (AnswerEnum answer in answersDictionary.Keys)
            {
                Answer.GetAnswerFromEnum(answer).DrawAnswerButton(answersDictionary[answer]);
                
            }
        }
    }
}