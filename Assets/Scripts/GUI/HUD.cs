using SparuvianConnection.Adoptatron.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SparuvianConnection.Adoptatron.GUI
{
    public class HUD : MonoBehaviour
    {
        public TMP_Text sitSkillText;
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
    }
}