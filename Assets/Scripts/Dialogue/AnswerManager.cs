using SparuvianConnection.Adoptatron.Gameplay;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Dialogue
{
    public class AnswerManager
    {
        #region Singleton pattern
        private static AnswerManager _current;
        public static AnswerManager Instance
        {
            get
            {
                if (_current == null)
                {
                    _current = new AnswerManager();
                }
                return _current;
            }
        }
        #endregion

        #region Answer: Yes
        private DialogueNode _yesNode;
        public void SetYesAnswerDialogue(DialogueNode node)
        {
            _yesNode = node;
        }
        
        public void HandleYesAnswerSelected()
        {
            GameEvents.Instance.TriggerStartDialogueEvent(_yesNode);
        }
        #endregion

        #region Answer: No
        private DialogueNode _noNode;
        public void SetNoAnswerDialogue(DialogueNode node)
        {
            _noNode = node;
        }
        
        public void HandleNoAnswerSelected()
        {
            GameEvents.Instance.TriggerStartDialogueEvent(_noNode);
        }
        #endregion
        
    }
}