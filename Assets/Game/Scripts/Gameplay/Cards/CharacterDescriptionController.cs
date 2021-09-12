using HCPJ3.Tools;
using UnityEngine;
using NaughtyAttributes;

namespace HCPJ3
{
    public class CharacterDescriptionController : MonoBehaviour
    {
        [SerializeField] private string[] _nameCollection = { };
        [SerializeField] private string[] _surnameCollection = { };
        [Space]
        [SerializeField] [Range (0, 1)] private float _punkQuoteChance = 0.5f;
        [SerializeField] private string[] _punkQuotes = { };
        [SerializeField] private string[] _copQuotes = { };
        [SerializeField] private string[] _mixedQuotes = { };

        private CharacterDescriptionDisplayable _displayable;

        private RandomList<string> _randomPunkQuotes;
        private RandomList<string> _randomCopQuotes;
        private RandomList<string> _randomMixedQuotes;

        private void Awake ()
        {
            Initialize ();
        }

        private void Initialize ()
        {
            _displayable = GetComponentInChildren<CharacterDescriptionDisplayable> ();
            
            _randomPunkQuotes = new RandomList<string>(_punkQuotes);
            _randomCopQuotes = new RandomList<string>(_copQuotes);
            _randomMixedQuotes = new RandomList<string>(_mixedQuotes);
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

            // randomize name
            _displayable.SetName ($"{GetRandomString(_nameCollection)} {GetRandomString(_surnameCollection)}");

            // randomize description
            int copQuoteIndex = isCop ? Random.Range (0, 3) : -1;

            for (int i = 0; i < 3; i++)
            {
                string description = string.Empty;
                if (i == copQuoteIndex)
                {
                    description = $"{GetRandomQuote(_randomCopQuotes)}";
                } else
                {
                    if (isCop)
                    {
                        description = $"{GetRandomQuote(_randomMixedQuotes)}"; // cops can't say punk quotes kiddo
                    } else
                    {
                        if (Random.value < _punkQuoteChance)
                        {
                            description = $"{GetRandomQuote(_randomPunkQuotes)}";
                        }
                        else
                        {
                            description = $"{GetRandomQuote(_randomMixedQuotes)}";
                        }
                    }
                }

                _displayable.SetDescription (description, i);
            }
        }

        private string GetRandomQuote(RandomList<string> quotes)
        {
            return quotes.Next();
        }

        private string GetRandomString (string[] stringArray)
        {
            return stringArray[Random.Range (0, stringArray.Length)];
        }
    }
}