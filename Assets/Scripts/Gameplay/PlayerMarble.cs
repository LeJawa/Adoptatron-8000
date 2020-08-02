using SparuvianConnection.Adoptatron.Gameplay.Marbles;
using SparuvianConnection.Adoptatron.Utils;
using Unity.Mathematics;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Gameplay
{
    public class PlayerMarble : Marble
    {
        private Camera _cameraMain;
        
        private bool _startingToPull = false;
        private Vector2 _startingPullPositionInScreenCoordinates = new Vector2();
        private Vector2 _pullDirectionInScreenCoordinates;
        private Vector2 _pullDirection = new Vector2();

        public float maxImpulse = 5f;
        public float maxPullDistance = 2f;

        private LineRenderer _trajectory;

        public GameObject prefabStartingPullPosition;
        private GameObject _startingPullPositionGO;

        private bool _canStillShoot = true;


        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _cameraMain = Camera.main;

            _trajectory = GetComponent<LineRenderer>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (_canStillShoot)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    StartPulling();
                }
                else if (_startingToPull && Input.GetMouseButtonUp(0))
                {
                    ReleaseBall();
                }
                else if (Input.GetMouseButton(0))
                {
                    UpdatePullDirection();
                }
            }
            
            
        }

        private void ReleaseBall()
        {
            _rb2d.AddForce(_pullDirection * maxImpulse, ForceMode2D.Impulse);
            _startingToPull = false;
            _trajectory.SetPosition(1, Vector3.zero);
            
            Destroy(_startingPullPositionGO);
            
            GameEvents.Instance.TriggerNewPlayerShotEvent();
        }

        private void StartPulling()
        {
            _startingToPull = true;
            _startingPullPositionInScreenCoordinates = Input.mousePosition;

            Vector3 startingPullPosition = _cameraMain.ScreenToWorldPoint(_startingPullPositionInScreenCoordinates);
            startingPullPosition.z = 0;
            
            
            if (_startingPullPositionGO == null)
            {
                _startingPullPositionGO = Instantiate(prefabStartingPullPosition, startingPullPosition, quaternion.identity);
            }
            else // Can happen if release frame is skipped
            {
                _startingPullPositionGO.transform.position = startingPullPosition;
            }
        }

        private void UpdatePullDirection()
        {
            _pullDirectionInScreenCoordinates = _startingPullPositionInScreenCoordinates - (Vector2) Input.mousePosition;

            _pullDirection = Vector2.ClampMagnitude(vector: ScreenUtils.ScreenToWorldVector(_pullDirectionInScreenCoordinates), maxLength: maxPullDistance);
            
            _trajectory.SetPosition(1, _pullDirection);
        }

        public void CannotShootAnymore()
        {
            _canStillShoot = false;
        }
        
        public void CanShootAgain()
        {
            _canStillShoot = true;
        }
    }
}
