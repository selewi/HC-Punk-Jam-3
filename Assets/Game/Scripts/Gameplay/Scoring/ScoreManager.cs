using DG.Tweening;
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

        [SerializeField]
        private AudioManager _audioManager;

        private ScoreDisplayable _displayable = null;

        public void Initialize ()
        {
            _currentScore = 0;
            _displayable = GetComponentInChildren<ScoreDisplayable> ();
            _displayable.SetValue(_currentScore);
        }

        private void Start ()
        {
            _displayable.SetValue (_currentScore);
        }

        public void AddScore (int addedScore)
        {
            _currentScore = Mathf.Max (0, _currentScore + addedScore);
            _displayable.SetValue (_currentScore);
            _audioManager.SetMusicIntensity(_currentScore);
        }

        public void HandleCopFound ()
        {
            _displayable.AnimateFind();
            AddScore (_copFoundScoreGiven);
        }

        public void HandleCopMiss ()
        {
            AddScore (_copMissScoreGiven);
        }

        public void HandlePunkFound ()
        {
            _displayable.AnimateFind();
            AddScore (_punkFoundScoreGiven);
        }

        public void HandlePunkMiss ()
        {
            _displayable.AnimateMiss();
            AddScore (_punkMissScoreGiven);
        }
    }
}