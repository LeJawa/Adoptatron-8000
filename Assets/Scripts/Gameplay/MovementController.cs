using System;
using SparuvianConnection.Adoptatron.Utils;
using Unity.Mathematics;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Gameplay
{
    public class MovementController : MonoBehaviour
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


        // Start is called before the first frame update
        private void Start()
        {
            _cameraMain = Camera.main;
            
            _playerMarble = GameObject.FindWithTag("Player").GetComponent<PlayerMarble>();
            _trajectory = GameObject.FindWithTag("Player").GetComponent<LineRenderer>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (!_startingToPull)
            {
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
        }


        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (_playerMarble.CanShoot)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartPulling();
                    }
                }
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
            _startingPullPositionInScreenCoordinates = Input.mousePosition;

            Vector3 startingPullPosition = _cameraMain.ScreenToWorldPoint(_startingPullPositionInScreenCoordinates);
            startingPullPosition.z = 0;
            
            transform.position = startingPullPosition;
        }

        private void UpdatePullDirection()
        {
            _pullDirectionInScreenCoordinates = _startingPullPositionInScreenCoordinates - (Vector2) Input.mousePosition;

            _pullDirection = Vector2.ClampMagnitude(vector: ScreenUtils.ScreenToWorldVector(_pullDirectionInScreenCoordinates), maxLength: maxPullDistance);
            
            _trajectory.SetPosition(1, _pullDirection);
        }
    }
}