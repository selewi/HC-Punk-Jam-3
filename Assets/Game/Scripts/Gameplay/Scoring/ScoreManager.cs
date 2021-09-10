using UnityEngine;
using NaughtyAttributes;

namespace HCPJ3
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] [ReadOnly] private int _currentScore = 0;
        [SerializeField] private int _punkFoundScoreGiven = 100;
        [SerializeField] private int _punkMissScoreGiven = -50;
        [SerializeField] private int _copFoundScoreGiven = 300;
        [SerializeField] private int _copMissScoreGiven = -150;

        private ScoreDisplayable _displayable = null;

        public void Initialize ()
        {
            _displayable = GetComponentInChildren<ScoreDisplayable> ();
        }

        private void Start ()
        {
            _displayable.SetValue (_currentScore);
        }

        public void AddScore (int addedScore)
        {
            _currentScore = Mathf.Max (0, _currentScore + addedScore);
            _displayable.SetValue (_currentScore);
        }

        public void HandleCopFound ()
        {
            AddScore (_copFoundScoreGiven);
        }

        public void HandleCopMiss ()
        {
            AddScore (_copMissScoreGiven);
        }

        public void HandlePunkFound ()
        {
            AddScore (_punkFoundScoreGiven);
        }

        public void HandlePunkMiss ()
        {
            AddScore (_punkMissScoreGiven);
        }
    }
}