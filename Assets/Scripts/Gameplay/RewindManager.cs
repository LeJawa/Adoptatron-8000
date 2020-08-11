using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SparuvianConnection.Adoptatron.Utils;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Gameplay
{
    public class RewindManager : MonoBehaviour
    {

        private List<GameObject> _marbles = new List<GameObject>();

        private Dictionary<GameObject, Stack<Vector2>> _positionRecords = new Dictionary<GameObject, Stack<Vector2>>();
        private int _numberOfRecords = 0;
        private bool _recording = false;

        private float _timer = 0;
        private float _timeBetweenRecordings = 1f;

        private float _maxRewindDeltaTime = 0.1f;
        private float _rewindDeltaTime = 0.1f;

        public float rewindTime = 5f;

        private void Start()
        {
            Initialize(0.2f);
        }

        public void Initialize(float timeBetweenRecordings)
        {
            _timeBetweenRecordings = timeBetweenRecordings;
            
            _marbles.Add(GameObject.FindWithTag("Player"));

            foreach (GameObject marble in GameObject.FindGameObjectsWithTag("Marble"))
            {
                _marbles.Add(marble);
            }

            foreach (GameObject marble in _marbles)
            {
                _positionRecords.Add(marble, new Stack<Vector2>());
                _positionRecords[marble].Push(marble.transform.position);
            }

            _recording = false;
            _numberOfRecords++;
        }

        private void Update()
        {
            if (_recording)
            {
                if (_timer >= _timeBetweenRecordings)
                {
                    RecordPositions();
                }
                else
                {
                    _timer += Time.deltaTime;
                }
            }
        }

        public void StartRecording()
        {
            _recording = true;
            RecordPositions();
        }

        public void StopRecording()
        {
            _recording = true;
        }

        public void RecordPositions()
        {
            foreach (GameObject marble in _marbles)
            {
                _positionRecords[marble].Push(marble.transform.position);
            }

            _timer = 0;
            _numberOfRecords++;
        }

        public void StartRewind()
        {
            _recording = false;
            
            CoroutineHelper.Instance.StartCoroutine(RewindCoroutine());
        }

        private IEnumerator RewindCoroutine()
        {
            Time.timeScale = 0;

            _rewindDeltaTime = rewindTime / _positionRecords[_positionRecords.Keys.First()].Count;

            _rewindDeltaTime = _rewindDeltaTime > _maxRewindDeltaTime ? _maxRewindDeltaTime : _rewindDeltaTime;
            
            WaitForSecondsRealtime wait = new WaitForSecondsRealtime(_rewindDeltaTime);

            while (_numberOfRecords > 0)
            {
                foreach (GameObject marble in _marbles)
                {
                    marble.transform.position = _positionRecords[marble].Pop();
                }

                _numberOfRecords--;
                yield return wait;
            }
            
            GameEvents.Instance.TriggerAllMarblesStopEvent();

            Time.timeScale = 1;

            Debug.Log("Finished rewind");
        }
    }
}