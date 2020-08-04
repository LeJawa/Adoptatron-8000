using UnityEngine;
using UnityEngine.UI;

namespace SparuvianConnection.Adoptatron.Dialogue
{
    public abstract class Answer
    {
        public static Answer GetAnswerFromEnum(AnswerEnum answer)
        {
            switch (answer)
            {
                case AnswerEnum.Yes:
                    return new Yes();
                case AnswerEnum.No:
                    return new No();
                default:
                    return null;
            }
        }

        protected Button DrawAnswerButton(string answer)
        {
            return ((GameObject)Object.Instantiate(Resources.Load(@"Dialogues\" + answer + "Button"), GameObject.FindWithTag("Answers").transform)).GetComponent<Button>();
        }

        public abstract void DrawAnswerButton(DialogueNode dialogueNode);

    }

    public class Yes : Answer
    {
        public override void DrawAnswerButton(DialogueNode dialogueNode)
        {
            Button button = DrawAnswerButton("Yes");
            AnswerManager.Instance.SetYesAnswerDialogue(dialogueNode);
            button.onClick.AddListener(AnswerManager.Instance.HandleYesAnswerSelected);
        }
    }
    public class No : Answer
    {
        public override void DrawAnswerButton(DialogueNode dialogueNode)
        {
            Button button = DrawAnswerButton("No");
            button.onClick.AddListener(AnswerManager.Instance.HandleNoAnswerSelected);
        }
    }
}