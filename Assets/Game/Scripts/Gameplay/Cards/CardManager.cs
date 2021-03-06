using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using HCPJ3.Tools;

namespace HCPJ3
{
    public class CardManager : MonoBehaviour
    {
        public enum Direction
        {
            None,
            Left,
            Right
        }

        [SerializeField]
        private CardController[] _cards;

        [SerializeField]
        private GameplayManager _gameplayManager;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private int _borderSize = 500;

        [Range(0, 100)]
        [SerializeField]
        private int _copperChance;

        [Header("Game Events")]

        [SerializeField]
        private GameEvent _cardSelected;

        [SerializeField]
        private GameEvent _cardReleased;

        private int _currentCardIndex;
        private bool _dragging;
        private bool _canDrag = true;

        [HideInInspector]
        public UnityEvent<CardReleaseEventInfo> onCardRelease = null;

        public void Initialize()
        {
            _currentCardIndex = 0;
            InitializeAllCards ();
            DisplayNewCard ();
        }

        private void Update()
        {
            if (!_gameplayManager.IsRunning) return;
            if (_canDrag == false) return;

            if (Input.GetMouseButton(0))
            {
                CardController card = _cards[_currentCardIndex];
                
                // Card just been picked up
                if (!_dragging)
                {
                    PickCard(card);
                }

                _dragging = true;
                Vector2 position = _camera.ScreenToWorldPoint(Input.mousePosition);
                card.transform.position = Vector3.right * Mathf.Lerp (card.transform.position.x, position.x, Time.deltaTime * 10.0f);
                card.transform.rotation = Quaternion.Lerp (card.transform.rotation, Quaternion.Euler (0, 0, -position.x * 2), Time.deltaTime * 5.0f);
                card.SwipeTextController.SetVisibility (Mathf.Abs(card.transform.position.x)); // using card transform position to get smooth transition
                card.SwipeTextController.UpdateText (card.transform.position.x < 0);
            }
            else if (_dragging)
            {
                _dragging = false;
                HandleRelease(_cards[_currentCardIndex]);
            }

            RefreshCard(_cards[_currentCardIndex]);
        }

        private void PickCard(CardController card)
        {
            _cardSelected.Raise();
            card.Outline.SetVisible(true);
            card.AnimateOnPick();
            card.SetSortingOrder(50);
        }

        private void RefreshCard(CardController card)
        {
            card.Outline.SetDirection(GetSwipeDirection(card));
        }

        private void DisplayNewCard()
        {
            CardController currentCard = _cards[_currentCardIndex];
            currentCard.Randomize(Random.Range(0, 101) <= _copperChance);
            MoveBack(currentCard);
            
            _currentCardIndex = (_currentCardIndex + 1) % _cards.Length;
            
            currentCard = _cards[_currentCardIndex]; 
            MoveFront(currentCard);
        }

        private void InitializeAllCards ()
        {
            foreach (var card in _cards)
            {
                card.AnimateOnRelease(transition: false);
                card.Randomize (Random.Range (0, 101) <= _copperChance);
            }
        }

        private static void MoveFront(CardController card)
        {
            card.transform.localPosition = Vector3.zero;
            card.SetSortingOrder (1);
        }

        private static void MoveBack(CardController card)
        {
            card.transform.localPosition = Vector3.zero;
            card.transform.localRotation = Quaternion.identity;
            card.AnimateOnRelease (transition: false);
            card.SetSortingOrder (-1);
        }

        private void HandleRelease(CardController card)
        {
            Direction dir = GetSwipeDirection(card);
            if (dir == Direction.None)
            {
                card.SwipeTextController.SetVisibility (0);
                card.AnimateOnRelease (transition: true);
                card.SetSortingOrder(1);
                _cardReleased.Raise();
            }
            else
            {
                _canDrag = false;

                _cards[_currentCardIndex].transform
                    .DOMoveX (dir == Direction.Right ? 10 : -10, 0.25f)
                    .OnComplete (HandleReleaseAnimationComplete);

                onCardRelease?.Invoke (
                    new CardReleaseEventInfo(
                        cardIsCop: card.IsCop,
                        swipeDirection: dir
                    )
                );
            }
        }

        private void HandleReleaseAnimationComplete ()
        {
            _canDrag = true;
            DisplayNewCard ();
        }

        private Direction GetSwipeDirection(CardController card)
        {
            Vector2 screenPos = _camera.WorldToScreenPoint(card.transform.position);
            
            if (screenPos.x <= (Screen.width / 2) - _borderSize)
            {
                return Direction.Left;
            }
            
            if (screenPos.x > (Screen.width / 2) + _borderSize)
            {
                return Direction.Right;
            }

            return Direction.None;
        }

        public struct CardReleaseEventInfo
        {
            private bool _cardIsCop;
            private Direction _swipeDirection;

            public bool CardIsCop => _cardIsCop;
            public bool SwipeIsCorrect
            {
                get
                {
                    if (CardIsCop)
                    {
                        return _swipeDirection == Direction.Left;
                    } else
                    {
                        return _swipeDirection == Direction.Right;
                    }
                }
            }

            public CardReleaseEventInfo (bool cardIsCop, Direction swipeDirection)
            {
                _cardIsCop = cardIsCop;
                _swipeDirection = swipeDirection;
            }
        }
    }
}
