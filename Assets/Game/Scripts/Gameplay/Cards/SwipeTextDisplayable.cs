using UnityEngine;
using TMPro;

namespace HCPJ3
{
    public class SwipeTextDisplayable : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _valueTMP = null;
        [Space]
        [SerializeField] private Color _copBustedColor = Color.red;
        [SerializeField] private Color _legitPunkColor = Color.green;

        private float _visibilityOffset = 0.5f;
        private Color _targetColor = Color.white;

        public void SetText (string newText, bool isCop)
        {
            _valueTMP.text = newText;
            _targetColor = isCop ? _copBustedColor : _legitPunkColor;
        }

        public void SetVisibility (float value)
        {
            _valueTMP.color = Color.Lerp (Color.clear, _targetColor, (value - _visibilityOffset));
        }
    }
}