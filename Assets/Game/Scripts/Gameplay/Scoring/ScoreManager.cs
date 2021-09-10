using UnityEngine;
using NaughtyAttributes;

namespace HCPJ3
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] [ReadOnly] private int _currentScore = 0;

        private ScoreDisplayable _displayable = null;

        private void Awake ()
        {
            _displayable = GetComponentInChildren<ScoreDisplayable> ();
        }

        private void Start ()
        {
            _displayable.SetValue (_currentScore);
        }

        public void AddScore (int addedScore)
        {
            _currentScore += addedScore;
            _displayable.SetValue (_currentScore);
        }
    }
}