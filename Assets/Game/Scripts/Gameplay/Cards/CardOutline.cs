using System;
using HCPJ3.Tools;
using NaughtyAttributes;
using UnityEngine;
using DG.Tweening;
using Direction = HCPJ3.CardManager.Direction;

namespace HCPJ3
{
    public class CardOutline : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private Color _defaultColor = Color.white;

        [SerializeField]
        private Color _negativeColor = Color.red;

        [SerializeField]
        private Color _positiveColor = Color.green;

        [SerializeField]
        private GameEvent _cardSwipeable;

        private Direction _direction = Direction.None;

        public void SetVisible(bool visible)
        {
            _spriteRenderer.enabled = visible;
        }

        public void SetDirection(Direction direction)
        {
            if (direction != Direction.None && direction != _direction)
            {
                _cardSwipeable.Raise();
            }

            switch (direction)
            {
                case Direction.Left:
                    _spriteRenderer.DOColor (_negativeColor, .25f);
                    break;
                case Direction.Right:
                    _spriteRenderer.DOColor (_positiveColor, .25f);
                    break;
                case Direction.None:
                    _spriteRenderer.DOColor (_defaultColor, .25f);
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(direction));
            }

            _direction = direction;
        }

        #region Editor

        [Button("Toggle Outline")]
        private void Toggle()
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
        }

        #endregion Editor
    }
}
