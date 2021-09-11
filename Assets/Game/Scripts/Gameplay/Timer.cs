using System;
using UnityEngine;

namespace HCPJ3
{
    public class Timer
    {
        private float _timeRemaining;
        private bool _isRunning;

        private Action OnTimerEnd;

        public void Start(float time, Action callback)
        {
            _timeRemaining = time;
            _isRunning = true;

            OnTimerEnd += callback;
        }

        public void Update()
        {
            if (_isRunning)
            {
                if (_timeRemaining > 0)
                {
                    _timeRemaining -= Time.deltaTime;
                }
                else
                {
                    _timeRemaining = 0;
                    _isRunning = false;

                    OnTimerEnd?.Invoke();
                    OnTimerEnd = null;
                }
            }
        }
    }
}
