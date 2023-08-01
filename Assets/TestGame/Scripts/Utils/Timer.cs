using System;
using UnityEngine;

namespace TestGame.Scripts.Utils
{
    [Serializable]
    public class Timer
    {
        [SerializeField] private float _value;
        private float _timeUp;

        public float Value
        {
            get => _value;
            set => _value = value;
        }

        public float RemainingTime => Mathf.Max(_timeUp - Time.time, 0);
        public bool IsReady => _timeUp <= Time.time;

        public void Reset()
        {
            _timeUp = Time.time + _value;
        }

        public void EarlyComplete()
        {
            _timeUp -= _value;
        }
    }
}