using SparuvianConnection.Adoptatron.Gameplay;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Dialogue
{
    public class EndDialogue : DialogueTester
    {
        private DogData _dogData;
        
        protected override void Start()
        {
            base.Start();

            _dogData = FindObjectOfType<DogData>();
            GenerateDialogue();
        }

        private void GenerateDialogue()
        {
            DialogueScriptableObject dialogue = ScriptableObject.CreateInstance<DialogueScriptableObject>();

            var time = _dogData.Dog.TotalTime;
            var rewindCount = _dogData.Dog.RewindCount;

            dialogue.dialogueText = "Total time was " + time + "\nAnd rewind count: " + rewindCount;
            _dialogueNode._dialogue = dialogue;
        }
    }
}