using SparuvianConnection.Adoptatron.Gameplay;
using SparuvianConnection.Adoptatron.States;
using SparuvianConnection.Adoptatron.Utils;
using TMPro;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Dialogue
{
    public class DialogueTester : MonoBehaviour
    {
        public TMP_Text text;
        private TextWriter.TextWriterSingle textWriterSingle;
        public Hints hints;

        public DialogueNode _dialogueNode;

        public DialogueState State { get; private set; }
        

        private float _hintTimer = 0f;
        public float timeBeforeShowingHints = 3f;

        private bool _showingHints = false;

        private void Start()
        {
            GameEvents.Instance.OnStartDialogue += StartNewDialogue;
            GameEvents.Instance.OnDialogueStateChange += ChangeDialogueState;
            
            State = new NormalDialogue();
        }

        private void ChangeDialogueState(DialogueState state)
        {
            State = state;
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
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                ShowNextLine();
                State.StopShowingHints(hints);
                _showingHints = false;
                _hintTimer = 0;

            }
            
            if (!_showingHints)
            {
                if (_hintTimer >= timeBeforeShowingHints)
                {
                    State.ShowHints(hints);
                    _showingHints = true;
                }
                else
                {
                    _hintTimer += Time.deltaTime;
                }
            }
            
        }
    }
}