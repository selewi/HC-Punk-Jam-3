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
        private Tween _punchTween = null;

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
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;

            _punchTween?.Complete ();
            _punchTween = transform.DOPunchRotation (new Vector3 (0, 0, 20f), 0.5f);

            Sequence colorSequence = DOTween.Sequence ();
            colorSequence.Append (_valueTMP.DOColor (_negativeColor, 0.2f));
            colorSequence.Append (_valueTMP.DOColor (Color.white, 0.2f));
            colorSequence.Play ();

            Sequence scaleSequence = DOTween.Sequence ();
            scaleSequence.Append (_valueTMP.transform.DOScale (new Vector3 (0.5f, 0.5f, 1), 0.2f));
            scaleSequence.Append (_valueTMP.transform.DOScale (Vector3.one, 0.2f));
            scaleSequence.Play ();
        }

        public void AnimateFind()
        {
            transform.localScale = Vector3.one;
            transform.rotation = Quaternion.identity;

            transform.DOPunchScale(new Vector3(1, 1, 0), 0.5f);

            Sequence colorSequence = DOTween.Sequence ();
            colorSequence.Append (_valueTMP.DOColor (_positiveColor, 0.2f));
            colorSequence.Append (_valueTMP.DOColor (Color.white, 0.2f));
            colorSequence.Play ();
        }
    }
}