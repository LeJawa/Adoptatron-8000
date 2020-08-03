using System;
using SparuvianConnection.Adoptatron.Gameplay;
using SparuvianConnection.Adoptatron.Gameplay.Skills;
using SparuvianConnection.Adoptatron.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SparuvianConnection.Adoptatron.GUI
{
    public class HUD : MonoBehaviour
    {
        [Serializable]
        public struct SkillGUI
        {
            public Button Button;
            public SkillName Name;
            public TMP_Text textObject;

            public SkillGUI(Button button, TMP_Text textObject, SkillName name)
            {
                this.Button = button;
                this.textObject = textObject;
                this.Name = name;
            }
        }
        
        public TMP_Text comboText;

        public SkillGUIDictionary skillGUIDictionary;


        public void ChangeSkillMasteryTo(SkillName skillName, int mastery)
        {
            var skillGUI = skillGUIDictionary[skillName];
            skillGUI.textObject.text = " " + skillGUI.Name.ToString() + ": " + mastery;
        }

        public void ChangeComboTo(int combo)
        {
            comboText.text = "Combo: " + combo;
        }

        public void HandleNextLevelButtonPressed()
        {
            GameEvents.Instance.TriggerLoadNextLevelEvent();
        }

        public void HandleSitSkillButtonPressed()
        {
            GameEvents.Instance.TriggerSkillPowerUpActivatedEvent(SkillName.Sit);
        }

        public void HandleComeSkillButtonPressed()
        {
            GameEvents.Instance.TriggerSkillPowerUpActivatedEvent(SkillName.Come);
        }

        private void SetSkillButtonInteractableTo(SkillName skillName, bool state)
        {
            MakeSureTheSkillButtonExists(skillName);

            skillGUIDictionary[skillName].Button.interactable = state;
        }

        private void MakeSureTheSkillButtonExists(SkillName skillName)
        {
            if (SkillButtonIsNotAvailable(skillName))
            {
                // ReSharper disable once NotAccessedVariable
                SkillGUI skillGui = skillGUIDictionary[skillName];
                string objectName = skillName + "SkillButton";
                skillGui.Button = GameObject.Find(objectName).GetComponent<Button>();
            }
        }

        private bool SkillButtonIsNotAvailable(SkillName skillName)
        {
            return skillGUIDictionary[skillName].Button == null;
        }

        public void ActivateSkillButton(SkillName skillName)
        {
            SetSkillButtonInteractableTo(skillName, true);
        }

        public void DeactivateSkillButton(SkillName skillName)
        {
            SetSkillButtonInteractableTo(skillName, false);
        }
    }
}