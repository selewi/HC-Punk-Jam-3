using NaughtyAttributes;
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
        [Label("UI Manager")]
        private UIManager _uiManager;

        private void Awake()
        {
            StartGame();
        }

        private void Start ()
        {
            _cardManager.onCardRelease.AddListener (HandleCardManagerCardRelease);
        }

        private void OnDestroy ()
        {
            _cardManager.onCardRelease.RemoveListener (HandleCardManagerCardRelease);
        }

        private void GameOver()
        {
            _uiManager.GameOver();
        }

        public void StartGame()
        {
            _cardManager.Initialize();
            _scoreManager.Initialize();
            _uiManager.Initialize();
        }

        private void HandleCardManagerCardRelease (CardManager.CardReleaseEventInfo eventInfo)
        {
            if (eventInfo.CardIsCop)
            {
                if (eventInfo.SwipeIsCorrect)
                {
                    _scoreManager.HandleCopFound ();
                } else
                {
                    GameOver();
                }
            } else
            {
                if (eventInfo.SwipeIsCorrect)
                {
                    _scoreManager.HandlePunkFound ();
                } else
                {
                    _scoreManager.HandlePunkMiss ();
                }
            }
        }
    }
}
