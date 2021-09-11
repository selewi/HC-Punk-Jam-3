using UnityEngine;
using NaughtyAttributes;

namespace HCPJ3
{
    public class PortraitController : MonoBehaviour
    {
        [Header ("Punk assets")]
        [SerializeField] private Sprite[] _backgroundCollection = { };
        [SerializeField] private Sprite[] _faceShapeCollection = { };
        [SerializeField] private Sprite[] _hairCollection = { };
        [SerializeField] private Sprite[] _eyesCollection = { };
        [SerializeField] private Sprite[] _noseCollection = { };
        [SerializeField] private Sprite[] _mouthCollection = { };
        [SerializeField] private Sprite[] _bodyCollection = { };
        [Header ("Cop only assets")]
        [SerializeField] private Sprite[] _copHairCollection = { };
        [SerializeField] private Sprite[] _copEyesCollection = { };
        [SerializeField] private Sprite[] _copNoseCollection = { };
        [SerializeField] private Sprite[] _copMouthCollection = { };
        [SerializeField] private Sprite[] _copBodyCollection = { };
        [Space]
        [SerializeField] [Range (0, 1)] private float _showCopClueChance = 0.5f;

        private PortraitDisplayable _displayable;

        private void Awake ()
        {
            Initialize ();
        }

        private void Initialize ()
        {
            _displayable = GetComponentInChildren<PortraitDisplayable> ();
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

        public void Randomize (bool isCop)
        {
            if (_displayable == null)
            {
                Initialize ();
            }

            _displayable.RandomizeAnimationStart ();
            _displayable.SetCharacterSprites (
                GetRandomSprite (_backgroundCollection),
                GetRandomSprite (_faceShapeCollection),
                GetRandomSprite (Random.value < _showCopClueChance ? _copHairCollection : _hairCollection),
                GetRandomSprite (Random.value < _showCopClueChance ? _copEyesCollection : _eyesCollection),
                GetRandomSprite (Random.value < _showCopClueChance ? _copNoseCollection : _noseCollection),
                GetRandomSprite (Random.value < _showCopClueChance ? _copMouthCollection :_mouthCollection),
                GetRandomSprite (Random.value < _showCopClueChance ? _copBodyCollection : _bodyCollection)
            ); ;
        }

        private Sprite GetRandomSprite (Sprite[] spriteArray)
        {
            if (spriteArray.Length == 0)
            {
                Debug.LogWarning ("There's an array without assets!");
                return null;
            }

            return spriteArray[Random.Range (0, spriteArray.Length)];
        }

    }
}