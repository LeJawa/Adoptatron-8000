using SparuvianConnection.Adoptatron.Utils;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Gameplay
{
    public class CursorController : MonoBehaviour
    {
        private Camera _cameraMain;
        
        private bool _startingToPull = false;
        private Vector2 _startingPullPositionInScreenCoordinates = new Vector2();
        private Vector2 _pullDirectionInScreenCoordinates;
        private Vector2 _pullDirection = new Vector2();

        public float maxImpulse = 5f;
        public float maxPullDistance = 2f;

        private LineRenderer _trajectory;

        private PlayerMarble _playerMarble;

        private bool _colliding = false;
        
        private void Start()
        {
            _cameraMain = Camera.main;
            
            _playerMarble = GameObject.FindWithTag("Player").GetComponent<PlayerMarble>();
            _trajectory = GameObject.FindWithTag("Player").GetComponent<LineRenderer>();
        }
        
        private void Update()
        {
#if UNITY_ANDROID
            if (Input.touchCount <= 0) return;
            
            Touch touch = Input.GetTouch(0);
            
            if (!_startingToPull)
            {
                    if (touch.phase == TouchPhase.Began && _playerMarble.CanShoot)
                    {
                        StartPulling();
                        return;
                    }
            }
            else
            {
                if (_startingToPull && touch.phase == TouchPhase.Ended)
                {
                    ReleaseBall();
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    UpdatePullDirection();
                }
            }
            
#else
            if (Input.GetKeyDown(KeyCode.KeypadMultiply))
            {
                GameManager.Instance.LoadNextLevel();
            }
            
            if (!_startingToPull)
            {
                if (_colliding)
                {
                    if (Input.GetMouseButtonDown(0) && _playerMarble.CanShoot)
                    {
                        StartPulling();
                        return;
                    }
                }
                
                Vector3 mousePosition = Input.mousePosition;

                mousePosition = _cameraMain.ScreenToWorldPoint(mousePosition);
                mousePosition.z = 0;

                transform.position = mousePosition;
            }
            else
            {
                if (_startingToPull && Input.GetMouseButtonUp(0))
                {
                    ReleaseBall();
                }
                else if (Input.GetMouseButton(0))
                {
                    UpdatePullDirection();
                }
            }


#endif
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _colliding = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _colliding = false;
            }
        }
        
        

        private void ReleaseBall()
        {
            _playerMarble.AddForce(_pullDirection * maxImpulse);
            _startingToPull = false;
            _trajectory.SetPosition(1, Vector3.zero);
            
            GameEvents.Instance.TriggerNewPlayerShotEvent();
        }

        private void StartPulling()
        {
            _playerMarble.StopMarbleMovement();
            
            _startingToPull = true;
#if UNITY_ANDROID
            _startingPullPositionInScreenCoordinates = Input.touches[0].position;
#else
            _startingPullPositionInScreenCoordinates = Input.mousePosition;
#endif
            Vector3 startingPullPosition = _cameraMain.ScreenToWorldPoint(_startingPullPositionInScreenCoordinates);
            startingPullPosition.z = 0;
            
            transform.position = startingPullPosition;
        }

        private void UpdatePullDirection()
        {
            _playerMarble.StopMarbleMovement();
#if UNITY_ANDROID
            _pullDirectionInScreenCoordinates = _startingPullPositionInScreenCoordinates - (Vector2) Input.touches[0].position;
#else
            _pullDirectionInScreenCoordinates = _startingPullPositionInScreenCoordinates - (Vector2) Input.mousePosition;
#endif

            _pullDirection = Vector2.ClampMagnitude(vector: ScreenUtils.ScreenToWorldVector(_pullDirectionInScreenCoordinates), maxLength: maxPullDistance);
            
            _trajectory.SetPosition(1, _pullDirection);
        }
    }
}