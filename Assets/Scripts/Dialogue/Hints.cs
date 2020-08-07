using TMPro;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Dialogue
{
    public class Hints : MonoBehaviour
    {

        private Animator _animator;
        private static readonly int ShowingHints = Animator.StringToHash("ShowingHints");

        private TMP_Text _textObject;


        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _textObject = GetComponentInChildren<TMP_Text>();
        }

        public void ShowHints()
        {
            _animator.SetBool(ShowingHints, true);
        }

        public void StopShowingHints()
        {
            _animator.SetBool(ShowingHints, false);
        }

        public void SetHintText(string hint)
        {
            _textObject.text = hint;
        }
        
    }
}