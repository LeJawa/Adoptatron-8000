using SparuvianConnection.Adoptatron.Gameplay;
using SparuvianConnection.Adoptatron.Utils;
using TMPro;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Dialogue
{
    public class DialogueTester : MonoBehaviour
    {
        public TMP_Text text;
        private TextWriter.TextWriterSingle textWriterSingle;

        public DialogueNode _dialogueNode;

        private void Start()
        {
            // _dialogueNode = Resources.Load<DialogueNode>(@"Nodes\node1");

            GameEvents.Instance.OnStartDialogue += StartNewDialogue;
        }

        private void StartNewDialogue(DialogueNode dialogue)
        {
            _dialogueNode = dialogue;
            _dialogueNode.StartDialogueFromBeginning();

            RemoveAnswers();
            
            ShowNextLine();
        }

        private void RemoveAnswers()
        {
            foreach (Transform child in AnswerManager.Instance.AnswersObjectTransform) {
                GameObject.Destroy(child.gameObject);
            }
        }

        public void HandleGetNextLineButtonPressed()
        {
            ShowNextLine();
        }

        private void ShowNextLine()
        {
            if (textWriterSingle != null && textWriterSingle.IsActive())
            {
                // Currently active TextWriter
                textWriterSingle.WriteAllAndDestroy();
            }
            else
            {
                string message = _dialogueNode.GetNextLine();
                if (message != null)
                {
                    textWriterSingle = TextWriter.AddWriter_Static(uiText: text, textToWrite: message,
                        timePerCharacter: .01f, invisibleCharacters: false, visibleCursor: true, removeWriterBeforeAdd: true,
                        onComplete: null);
                }
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                HandleGetNextLineButtonPressed();
            }
        }
    }
}