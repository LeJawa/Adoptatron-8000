using System;
using SparuvianConnection.Adoptatron.Dialogue;
using SparuvianConnection.Adoptatron.Gameplay.Marbles;
using SparuvianConnection.Adoptatron.Gameplay.Skills;
using SparuvianConnection.Adoptatron.States;

namespace SparuvianConnection.Adoptatron.Gameplay
{
    public class GameEvents
    {
        #region Singleton pattern
        private static GameEvents _current;
        public static GameEvents Instance
        {
            get
            {
                if (_current == null)
                {
                    _current = new GameEvents();
                }
                return _current;
            }
        }
        #endregion

        #region Action<Marble> OnMarbleCollision
        public event Action<SkillMarble> OnMarbleCollision;

        public void TriggerMarbleCollisionEvent(SkillMarble marble)
        {
            OnMarbleCollision?.Invoke(marble);
        }
        #endregion
        
        #region Action OnNewPlayerShot
        public event Action OnNewPlayerShot;

        public void TriggerNewPlayerShotEvent()
        {
            OnNewPlayerShot?.Invoke();
        }
        #endregion
        
        #region Action<int> OnLoadLevel
        public event Action<int> OnLoadLevel;

        public void TriggerLoadLevelEvent(int level)
        {
            OnLoadLevel?.Invoke(level);
        }
        #endregion
        
        #region Action OnLoadNextLevel
        public event Action OnLoadNextLevel;

        public void TriggerLoadNextLevelEvent()
        {
            OnLoadNextLevel?.Invoke();
        }
        #endregion
        
        #region Action OnLoadLastLevel
        public event Action OnLoadLastLevel;

        public void TriggerLoadLastLevelEvent()
        {
            OnLoadLastLevel?.Invoke();
        }
        #endregion
        
        #region Action<SkillName> OnSkillPowerUpActivated
        public event Action<SkillName> OnSkillPowerUpActivated;

        public void TriggerSkillPowerUpActivatedEvent(SkillName skillName)
        {
            OnSkillPowerUpActivated?.Invoke(skillName);
        }
        #endregion
        
        #region Action<SkillName> OnNewSkillPowerUpAvailable
        public event Action<SkillName> OnNewSkillPowerUpAvailable;

        public void TriggerNewSkillPowerUpAvailableEvent(SkillName skillName)
        {
            OnNewSkillPowerUpAvailable?.Invoke(skillName);
        }
        #endregion
        
        #region Action OnAllMarblesStop
        public event Action OnAllMarblesStop;

        public void TriggerAllMarblesStopEvent()
        {
            OnAllMarblesStop?.Invoke();
        }
        #endregion
        
        #region Action<DialogueNode> OnStartDialogue
        public event Action<DialogueNode> OnStartDialogue;

        public void TriggerStartDialogueEvent(DialogueNode node)
        {
            OnStartDialogue?.Invoke(node);
        }
        #endregion
        
        #region Action OnRewindStart
        public event Action OnRewindStart;

        public void TriggerRewindStartEvent()
        {
            OnRewindStart?.Invoke();
        }
        #endregion
        
        #region Action<State> OnDialogueStateChange
        public event Action<DialogueState> OnDialogueStateChange;

        public void TriggerDialogueStateChangeEvent(DialogueState state)
        {
            OnDialogueStateChange?.Invoke(state);
        }
        #endregion
        
        #region Action<DialogueEndAction> OnDialogueEndSetAction
        public event Action<DialogueEndAction> OnDialogueEndSetAction;

        public void TriggerDialogueEndSetActionEvent(DialogueEndAction action)
        {
            OnDialogueEndSetAction?.Invoke(action);
        }
        #endregion

    }
}