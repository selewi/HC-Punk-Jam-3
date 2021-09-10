using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

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
        private Camera _camera;

        [SerializeField]
        private CardController[] _cards;

        [SerializeField]
        private Transform _backgroundContainer;

        [SerializeField]
        private Transform _frontContainer;

        [SerializeField]
        private int _borderSize = 500;

        [Range(0, 100)]
        [SerializeField]
        private int _copperChance;

        private int _currentCardIndex;
        private bool _dragging;

        public UnityEvent<CardReleaseEventInfo> onCardRelease = null;

        public void Initialize()
        {
            _currentCardIndex = 0;
            DisplayNewCard();
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

        private void MoveFront(CardController card)
        {
            card.transform.SetParent(_frontContainer);
            card.transform.localPosition = Vector3.zero;
        }

        private void MoveBack(CardController card)
        {
            card.transform.SetParent(_backgroundContainer);
            card.transform.localPosition = Vector3.zero;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                _dragging = true;
                Vector2 position = _camera.ScreenToWorldPoint(Input.mousePosition);
                _cards[_currentCardIndex].transform.position = position;
            }
            else if (_dragging)
            {
                _dragging = false;
                HandleRelease(_cards[_currentCardIndex]);
            }
        }

        private void HandleRelease(CardController card)
        {
            Direction dir = GetSwipeDirection(card);
            if (dir == Direction.None)
            {
                card.transform.position = _frontContainer.position;
            }
            else
            {
                onCardRelease?.Invoke (
                    new CardReleaseEventInfo(
                        cardIsCop: card.IsCop,
                        swipeDirection: dir
                    )
                );
                DisplayNewCard ();
            }
        }

        private Direction GetSwipeDirection(CardController card)
        {
            Vector2 screenPos = _camera.WorldToScreenPoint(card.transform.position);
            
            if (screenPos.x <= _borderSize)
            {
                return Direction.Left;
            }
            
            if (screenPos.x > Screen.width - _borderSize)
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
