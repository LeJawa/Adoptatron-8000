namespace SparuvianConnection.Adoptatron.Gameplay
{
    public class EndGate : Gate
    {
        protected override void TriggerAppropriateEvent()
        {
            GameEvents.Instance.TriggerEndGateCrossedEvent();
        }
    }
}