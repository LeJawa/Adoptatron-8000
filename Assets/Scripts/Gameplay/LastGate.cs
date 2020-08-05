namespace SparuvianConnection.Adoptatron.Gameplay
{
    public class LastGate : Gate
    {
        protected override void TriggerAppropriateEvent()
        {
            GameEvents.Instance.TriggerLoadLastLevelEvent();
        }
    }
}