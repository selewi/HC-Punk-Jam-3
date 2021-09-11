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

            // randomize name
            _displayable.SetName (string.Format ("{0} {1}", GetRandomString (_nameCollection), GetRandomString (_surnameCollection)));

            // randomize description
            string description = string.Empty;
            int copQuoteIndex = isCop ? Random.Range (0, 3) : -1;

            for (int i = 0; i < 3; i++)
            {
                if (i == copQuoteIndex)
                {
                    description += string.Format ("{0}\n", GetRandomString (_copQuotes));
                } else
                {
                    if (isCop)
                    {
                        description += string.Format ("{0}\n", GetRandomString (_mixedQuotes)); // cops can't say punk quotes kiddo
                    } else
                    {
                        description += string.Format ("{0}\n", GetRandomString (Random.value < _punkQuoteChance ? _punkQuotes : _mixedQuotes));
                    }
                }
            }

            _displayable.SetDescription (description);
        }

        private string GetRandomString (string[] stringArray)
        {
            return stringArray[Random.Range (0, stringArray.Length)];
        }
    }
}