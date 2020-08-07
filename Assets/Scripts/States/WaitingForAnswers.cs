namespace SparuvianConnection.Adoptatron.States
{
    public class WaitingForAnswers : DialogueState
    {
        protected override void SetHintText()
        {
            hintText = "Please select an answer";
        }
    }
}