using DG.Tweening;
using UnityEngine;
using TMPro;

namespace HCPJ3
{
    public class ScoreDisplayable : MonoBehaviour
    { 
        [SerializeField] private TextMeshPro _valueTMP = null;

        [SerializeField]
        private Color _positiveColor;

        [SerializeField]
        private Color _negativeColor;

        private string _baseText = null;

        private void Awake ()
        {
            _baseText = _valueTMP.text;
        }

        public void SetValue (int score)
        {
            _valueTMP.text = _baseText.Replace ("{value}", score.ToString ());
        }

        public void AnimateMiss()
        {
            transform.DOPunchRotation(new Vector3(0, 0, 20f), 0.5f);
            
            _valueTMP.DOColor(_negativeColor, 0.2f).OnComplete(() => {
                _valueTMP.DOColor(Color.white, 0.2f);
            });
            
            _valueTMP.transform.DOScale(new Vector3(0.5f, 0.5f, 1), 0.2f).OnComplete(() => {
                _valueTMP.transform.DOScale(Vector3.one, 0.2f);
            });
        }

        public void AnimateFind()
        {
            transform.DOPunchScale(new Vector3(1, 1, 0), 0.5f);
            
            _valueTMP.DOColor(_positiveColor, 0.2f).OnComplete(() => {
                _valueTMP.DOColor(Color.white, 0.2f);
            });
        }
    }
}