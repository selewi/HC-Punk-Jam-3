using UnityEngine;

namespace HCPJ3
{
    public class SwipeTextController : MonoBehaviour
    {
        [SerializeField] private readonly string _copBustedText = "CYBER COP";
        [SerializeField] private readonly string _legitPunkText = "LEGIT PUNK";

        private SwipeTextDisplayable _displayable = null;

        private void Awake ()
        {
            _displayable = GetComponentInChildren<SwipeTextDisplayable> ();
        }

        public void UpdateText (bool isCop)
        {
            _displayable.SetText (isCop ? _copBustedText : _legitPunkText, isCop);
        }

        public void SetVisibility (float value)
        {
            _displayable.SetVisibility (value);
        }
    }
}