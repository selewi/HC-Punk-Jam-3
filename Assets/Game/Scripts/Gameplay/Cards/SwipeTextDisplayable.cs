using UnityEngine;
using TMPro;

namespace HCPJ3
{
    public class SwipeTextDisplayable : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _valueTMP = null;
        [SerializeField] private SpriteRenderer _copBadgeSpriteRenderer = null;
        [SerializeField] private SpriteRenderer _punkBadgeSpriteRenderer = null;
        [Space]
        [SerializeField] private Color _copBustedColor = Color.red;
        [SerializeField] private Color _legitPunkColor = Color.green;
        [SerializeField] private float _visibilityOffset = 0.5f;

        private Color _targetColor = Color.white;
        private bool _isCop = false;

        public void SetText (string newText, bool isCop)
        {
            _isCop = isCop;
            _valueTMP.text = newText;
            _targetColor = isCop ? _copBustedColor : _legitPunkColor;
        }

        public void SetVisibility (float value)
        {
            _valueTMP.color = Color.Lerp (Color.clear, _targetColor, ((value - _visibilityOffset) * 2));

            if (_isCop)
            {
                _copBadgeSpriteRenderer.color = Color.Lerp (Color.clear, _targetColor, ((value - _visibilityOffset) * 2));
                _punkBadgeSpriteRenderer.color = Color.clear;
            } else
            {
                _punkBadgeSpriteRenderer.color = Color.Lerp (Color.clear, _targetColor, ((value - _visibilityOffset) * 2));
                _copBadgeSpriteRenderer.color = Color.clear;
            }
        }
    }
}