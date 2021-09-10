using UnityEngine;
using TMPro;

namespace HCPJ3
{
    public class ScoreDisplayable : MonoBehaviour
    { 
        [SerializeField] private TextMeshPro _valueTMP = null;

        private string _baseText = null;

        private void Awake ()
        {
            _baseText = _valueTMP.text;
        }

        public void SetValue (int score)
        {
            _valueTMP.text = _baseText.Replace ("{value}", score.ToString ());
        }
    }
}