using HCPJ3.Tools;
using UnityEngine;

namespace HCPJ3
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField]
        private CardManager _cardManager;

        [SerializeField]
        private ScoreManager _scoreManager;

        [SerializeField]
        private AudioManager _audioManager;

        [Header("Game Events")]
        [SerializeField]
        private GameEvent _cardSwipeFailPunk;

        [SerializeField]
        private GameEvent _cardSwipeSuccessPunk;

        [SerializeField]
        private GameEvent _cardSwipeFailCop;

        [SerializeField]
        private GameEvent _cardSwipeSuccessCop;

        [SerializeField]
        private GameEvent _gameStarted;

        [SerializeField]
        private GameEvent _gameOver;

        public bool IsRunning { get; private set; }

        private void Start ()
        {
            _cardManager.onCardRelease.AddListener (HandleCardManagerCardRelease);
            
            StartGame();
        }

        private void OnDestroy ()
        {
            _cardManager.onCardRelease.RemoveListener (HandleCardManagerCardRelease);
        }

        private void GameOver()
        {
            IsRunning = false;
            _gameOver.Raise();
        }

        public void StartGame()
        {
            _audioManager.Initialize();
            _cardManager.Initialize();
            _scoreManager.Initialize();
            _gameStarted.Raise();
            IsRunning = true;
        }

        private void HandleCardManagerCardRelease (CardManager.CardReleaseEventInfo eventInfo)
        {
            if (eventInfo.CardIsCop)
            {
                if (eventInfo.SwipeIsCorrect)
                {
                    _cardSwipeSuccessCop.Raise();
                    _scoreManager.HandleCopFound ();
                } else
                {
                    _cardSwipeFailCop.Raise();
                    GameOver();
                }
            } else
            {
                if (eventInfo.SwipeIsCorrect)
                {
                    _cardSwipeSuccessPunk.Raise();
                    _scoreManager.HandlePunkFound ();
                } else
                {
                    _cardSwipeFailPunk.Raise();
                    GameOver();
                }
            }
        }
    }
}
