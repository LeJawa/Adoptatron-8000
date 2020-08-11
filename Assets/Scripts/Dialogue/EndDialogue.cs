using SparuvianConnection.Adoptatron.Gameplay;
using SparuvianConnection.Adoptatron.Gameplay.Skills;
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

            var patience = _dogData.Dog.GetMasteryOfSkill(SkillName.Patience);
            var mpatience = _dogData.Dog.GetMasteryOfSkill(SkillName.MPatience);
            var empatience = _dogData.Dog.GetMasteryOfSkill(SkillName.EMPatience);

            int totalPatience = patience + mpatience * 10 + empatience * 50;

            string message = "Total patience was " + totalPatience + "\n";

            if (totalPatience == 69)
            {
                message += "(Nice!)\n";
            }

            if (totalPatience < 10)
            {
                message += "WOW you have some skill!\n" +
                           "The best I can do is a fruit fly\n" +
                           "What?\n" +
                           "Did you expect a trophy or something\n" +
                           "Just because you know how to play video games, it doesn't mean you know how to raise a dog...\n";
                message += "\nEnjoy your new pet!";
            }
            else if (totalPatience < 100)
            {
                message += "Well I guess you might deserve some pet after all...\n" +
                           "What about a rat?\n" +
                           "It doesn't require much patience after all...\n";
                message += "\nEnjoy your new pet!";
            }
            else if (totalPatience < 500)
            {
                message += "I'm impressed!\n" +
                           "(not really)\n" +
                           "With that level of commitment you can have a cat\n" +
                           "I hope it might help you reevaluate your life decisions...\n";
                message += "\nEnjoy your new pet!";
            }
            else
            {
                message += "You have done it\n" +
                           "You have been proven worthy of the Lord Corgi our Saviour\n" +
                           "Take good care of him and nothing wrong could happen to you\n" +
                           "I tip my antenna to you\n";
            }



            dialogue.dialogueText = message;
            _dialogueNode._dialogue = dialogue;
        }
    }
}