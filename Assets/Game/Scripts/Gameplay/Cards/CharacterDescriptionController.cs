using UnityEngine;
using NaughtyAttributes;

namespace HCPJ3
{
    public class CharacterDescriptionController : MonoBehaviour
    {
        [SerializeField] [Range (0, 1)] private float _punkQuoteChance = 0.5f;
        [SerializeField] private string[] _punkQuotes = { };
        [SerializeField] private string[] _copQuotes = { };
        [SerializeField] private string[] _mixedQuotes = { };

        private CharacterDescriptionDisplayable _displayable;

        private void Awake ()
        {
            Initialize ();
        }

        private void Initialize ()
        {
            _displayable = GetComponentInChildren<CharacterDescriptionDisplayable> ();
        }

        [Button ("Randomize test (cop)")]
        private void TestRandomizeCop ()
        {
            Randomize (isCop: true);
        }

        [Button ("Randomize test (punk)")]
        private void TestRandomizePunk ()
        {
            Randomize (isCop: false);
        }

        public void SetPunkQuoteChance (int newValue)
        {
            _punkQuoteChance = Mathf.Clamp (newValue, 0, 1);
        }

        public void Randomize (bool isCop)
        {
            if (_displayable == null)
            {
                Initialize ();
            }

            string description = string.Empty;
            int copQuoteIndex = isCop ? Random.Range (0, 3) : -1;

            for (int i = 0; i < 3; i++)
            {
                if (i == copQuoteIndex)
                {
                    description += string.Format ("{0}\n", GetRandomQuote (_copQuotes));
                } else
                {
                    description += string.Format ("{0}\n", GetRandomQuote (Random.value < _punkQuoteChance ? _punkQuotes : _mixedQuotes));
                }
            }

            _displayable.SetText (description);
        }

        private string GetRandomQuote (string[] quoteArray)
        {
            return quoteArray[Random.Range (0, quoteArray.Length)];
        }
    }
}