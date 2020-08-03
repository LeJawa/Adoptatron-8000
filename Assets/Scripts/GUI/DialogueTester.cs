using SparuvianConnection.Adoptatron.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SparuvianConnection.Adoptatron.GUI
{
    public class DialogueTester : MonoBehaviour
    {
        public Text text;
        private TextWriter.TextWriterSingle textWriterSingle;
        
        private DialogueScriptableObject _dialogue;

        private void Start()
        {
            _dialogue = Resources.Load<DialogueScriptableObject>(@"Dialogues\dialogue1");
        }

        public void HandleGetNextLineButtonPressed()
        {

            if (textWriterSingle != null && textWriterSingle.IsActive())
            {
                // Currently active TextWriter
                textWriterSingle.WriteAllAndDestroy();
            }
            else
            {
                string message = _dialogue.GetNextLine();
                if (message != null)
                {
                    textWriterSingle = TextWriter.AddWriter_Static(uiText: text, textToWrite: message,
                        timePerCharacter: .01f, invisibleCharacters: true, removeWriterBeforeAdd: false, onComplete: null);
                }
            }
        }



    }
}