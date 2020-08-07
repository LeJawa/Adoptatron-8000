using System;
using SparuvianConnection.Adoptatron.Gameplay;
using SparuvianConnection.Adoptatron.States;
using SparuvianConnection.Adoptatron.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace SparuvianConnection.Adoptatron.Dialogue
{
    [CreateAssetMenu(menuName = "New dialogue node")]
    public class DialogueNode : ScriptableObject
    {
        public DialogueScriptableObject _dialogue;

        public AnswerDialogueDictionary answersDictionary;

        public DialogueEndAction onDialogueEnd;

        [NonSerialized] private bool _endOfDialogue = false;

        public string GetNextLine()
        {
            if (_endOfDialogue)
            {
                return null;
            }
            
            string message = _dialogue.GetNextLine();
            string nextLine = _dialogue.SeeNextLine();

            if (nextLine == null)
            {
                ShowAnswers();

                GameEvents.Instance.TriggerDialogueEndSetActionEvent(onDialogueEnd);
                
                _endOfDialogue = true;

            }

            return message;
        }

        public void StartDialogueFromBeginning()
        {
            _endOfDialogue = false;
            _dialogue.Initialize();
            
            GameEvents.Instance.TriggerDialogueStateChangeEvent(new NormalDialogue());
        }

        private void ShowAnswers()
        {
            foreach (AnswerEnum answer in answersDictionary.Keys)
            {
                Answer.GetAnswerFromEnum(answer).DrawAnswerButton(answersDictionary[answer]);
            }
            
            GameEvents.Instance.TriggerDialogueStateChangeEvent(new WaitingForAnswers());
        }
    }
}