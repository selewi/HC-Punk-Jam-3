using UnityEngine;
using NaughtyAttributes;

namespace HCPJ3
{
    public class PortraitController : MonoBehaviour
    {
        [SerializeField] private Sprite[] _backgroundCollection = { };
        [SerializeField] private Sprite[] _faceShapeCollection = { };
        [SerializeField] private Sprite[] _hairCollection = { };
        [SerializeField] private Sprite[] _eyesCollection = { };
        [SerializeField] private Sprite[] _noseCollection = { };
        [SerializeField] private Sprite[] _mouthCollection = { };
        [SerializeField] private Sprite[] _bodyCollection = { };

        private PortraitDisplayable _displayable;

        private void Awake ()
        {
            Initialize ();
        }

        private void Initialize ()
        {
            _displayable = GetComponentInChildren<PortraitDisplayable> ();
        }

        [Button ("Randomize test")]
        public void Randomize ()
        {
            if (_displayable == null)
            {
                Initialize ();
            }

            _displayable.SetCharacterSprites (
                GetRandomSprite (_backgroundCollection),
                GetRandomSprite (_faceShapeCollection),
                GetRandomSprite (_hairCollection),
                GetRandomSprite (_eyesCollection),
                GetRandomSprite (_noseCollection),
                GetRandomSprite (_mouthCollection),
                GetRandomSprite (_bodyCollection)
            ); ;
        }

        private Sprite GetRandomSprite (Sprite[] spriteArray)
        {
            return spriteArray[Random.Range (0, spriteArray.Length)];
        }

    }
}