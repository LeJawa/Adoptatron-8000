using SparuvianConnection.Adoptatron.Gameplay;
using SparuvianConnection.Adoptatron.Gameplay.Skills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SparuvianConnection.Adoptatron.GUI
{
    public class HUD : MonoBehaviour
    {
        public Button sitSkillButton;
        public TMP_Text sitSkillText;

        public Button comeSkillButton;
        public TMP_Text comeSkillText;
        
        public TMP_Text comboText;


        public void ChangeSitSkillMasteryTo(int mastery)
        {
            sitSkillText.text = "Sit: " + mastery;
        }
        
        public void ChangeComeSkillMasteryTo(int mastery)
        {
            comeSkillText.text = "Come: " + mastery;
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

            switch (skillName)
            {
                case SkillName.Sit:
                    sitSkillButton.interactable = state;
                    break;
                case SkillName.Come:
                    comeSkillButton.interactable = state;
                    break;
                default:
                    Debug.Log("No skill button could be toggled");
                    break;
            }
        }

        private void MakeSureTheSkillButtonExists(SkillName skillName)
        {
            if (SkillButtonIsNotAvailable(skillName))
            {
                switch (skillName)
                {
                    case SkillName.Sit:
                        sitSkillButton = GameObject.Find("SitSkillButton").GetComponent<Button>();
                        break;
                    case SkillName.Come:
                        comeSkillButton = GameObject.Find("ComeSkillButton").GetComponent<Button>();
                        break;
                    default:
                        Debug.Log("No skill button could be found");
                        break;
                }
            }
        }

        private bool SkillButtonIsNotAvailable(SkillName skillName)
        {
            switch (skillName)
            {
                case SkillName.Sit:
                    return sitSkillButton == null;
                case SkillName.Come:
                    return comeSkillButton == null;
                default:
                    return false;
            }
            
            
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