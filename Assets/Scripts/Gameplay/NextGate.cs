namespace SparuvianConnection.Adoptatron.Gameplay
{
    public class NextGate : Gate
    {
        protected override void TriggerAppropriateEvent()
        {
            GameEvents.Instance.TriggerLoadNextLevelEvent();
        }
    }
}