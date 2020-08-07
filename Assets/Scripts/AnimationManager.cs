using UnityEngine;

namespace SparuvianConnection.Adoptatron
{
    public class AnimationManager
    {
        #region Singleton pattern

        private static AnimationManager _current;
        public static AnimationManager Instance
        {
            get
            {
                if (_current == null)
                {
                    _current = new AnimationManager();
                }
                return _current;
            }
        }
        #endregion
        
        
        private Animator _animationAnim;
        private static readonly int End = Animator.StringToHash("end");

        public Animator Animator => _animationAnim;
        
        public void StartEndSceneAnimation()
        {
            Time.timeScale = 1;
            if (_animationAnim == null)
            {
                InitializeSceneAnimator();
            }

            _animationAnim.SetTrigger(End);
        }

        public void InitializeSceneAnimator() {
            _animationAnim = GameObject.FindGameObjectWithTag("IOAnimation").GetComponent<Animator>();
        }
    }
}