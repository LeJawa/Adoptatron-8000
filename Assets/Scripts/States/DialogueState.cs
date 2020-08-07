using SparuvianConnection.Adoptatron.Dialogue;

namespace SparuvianConnection.Adoptatron.States
{
    public abstract class DialogueState
    {
        protected string hintText;

        public DialogueState()
        {
            Initialize();
        }
        
        public void ShowHints(Hints hints)
        {
            hints.SetHintText(hintText);
            hints.ShowHints();
        }

        public void StopShowingHints(Hints hints)
        {
            hints.StopShowingHints();
        }

        private void Initialize()
        {
            SetHintText();
        }
        
        protected abstract void SetHintText();
    }
}