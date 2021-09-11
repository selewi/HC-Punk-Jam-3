using HCPJ3.Tools;
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
        private Texture2D _cursorTexture;

        public bool IsRunning { get; private set; }

        private void Awake()
        {
            SetCursorSprite();
        }

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
            _uiManager.GameOver();
        }

        public void StartGame()
        {
            _audioManager.Initialize();
            _cardManager.Initialize();
            _scoreManager.Initialize();
            _uiManager.Initialize();
            IsRunning = true;
        }

        private void SetCursorSprite()
        {
            Cursor.SetCursor(_cursorTexture, Vector2.one, CursorMode.ForceSoftware);
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
                    _scoreManager.HandlePunkMiss ();
                }
            }
        }
    }
}
