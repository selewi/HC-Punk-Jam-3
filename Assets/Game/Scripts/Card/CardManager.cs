using UnityEngine;

namespace HCPJ3
{
    public class CardManager : MonoBehaviour
    {
        private enum Direction
        {
            None,
            Left,
            Right
        }

        [SerializeField]
        private CardController[] _cards;

        [SerializeField]
        private Transform _backgroundContainer;

        [SerializeField]
        private Transform _frontContainer;

        [SerializeField]
        private int _borderSize = 500;
        
        private int _currentCardIndex;

        private bool _dragging;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void Initialize()
        {
            _currentCardIndex = 0;
            DisplayNewCard();
        }

        private void DisplayNewCard()
        {
            MoveBack(_cards[_currentCardIndex].transform);
            
            _currentCardIndex = (_currentCardIndex + 1) % _cards.Length;

            Card card = GenerateCard();
            CardController controller = _cards[_currentCardIndex]; 
            controller.Initialize(card);
            
            MoveFront(controller.transform);
        }

        private void MoveFront(Transform controller)
        {
            controller.SetParent(_frontContainer);
            controller.localPosition = Vector3.zero;
        }

        private void MoveBack(Transform controller)
        {
            controller.SetParent(_backgroundContainer);
            controller.localPosition = Vector3.zero;
        }

        private Card GenerateCard()
        {
            return null;
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

        private void HandleRelease(CardController controller)
        {
            Direction dir = GetSwipeDirection(controller);
            if (dir == Direction.None)
            {
                controller.transform.position = _frontContainer.position;
            }
            else
            {
                // TODO Handle scoring

                DisplayNewCard();
            }
        }

        private Direction GetSwipeDirection(CardController controller)
        {
            Vector2 screenPos = _camera.WorldToScreenPoint(controller.transform.position);
            
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
    }
}
