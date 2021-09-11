using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

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
        }

        private void Start ()
        {
            DisplayNewCard ();
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
            card.transform.localPosition = Vector3.zero;
            card.SetSortingOrder (1);
        }

        private void MoveBack(CardController card)
        {
            card.transform.localPosition = Vector3.zero;
            card.transform.localRotation = Quaternion.identity;
            card.SetSortingOrder (-1);
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                _dragging = true;
                Vector2 position = _camera.ScreenToWorldPoint(Input.mousePosition);
                _cards[_currentCardIndex].transform.position = Vector3.right * Mathf.Lerp (_cards[_currentCardIndex].transform.position.x, position.x, Time.deltaTime * 10.0f);
                _cards[_currentCardIndex].transform.rotation = Quaternion.Lerp (_cards[_currentCardIndex].transform.rotation, Quaternion.Euler (0, 0, -position.x * 2), Time.deltaTime * 5.0f);
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
                card.transform.DOMoveX (0, 0.25f);
                card.transform.DORotate (Vector3.zero, 0.25f);
            }
            else
            {
                _cards[_currentCardIndex].transform
                    .DOMoveX (dir == Direction.Right ? 10 : -10, 0.25f)
                    .OnComplete (DisplayNewCard);

                onCardRelease?.Invoke (
                    new CardReleaseEventInfo(
                        cardIsCop: card.IsCop,
                        swipeDirection: dir
                    )
                );
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
