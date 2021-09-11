using System;
using NaughtyAttributes;
using UnityEngine;
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

        public void SetVisible(bool visible)
        {
            _spriteRenderer.enabled = visible;
        }

        public void SetDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    _spriteRenderer.color = _negativeColor;
                    break;
                case Direction.Right:
                    _spriteRenderer.color = _positiveColor;
                    break;
                case Direction.None:
                    _spriteRenderer.color = _defaultColor;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(direction));
            }
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
