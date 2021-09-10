using UnityEngine;

namespace HCPJ3
{
    public class PortraitDisplayable : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _backgroundSpriteRenderer = null;
        [SerializeField] private SpriteRenderer _faceShapeSpriteRenderer = null;
        [SerializeField] private SpriteRenderer _hairSpriteRenderer = null;
        [SerializeField] private SpriteRenderer _eyesSpriteRenderer = null;
        [SerializeField] private SpriteRenderer _noseSpriteRenderer = null;
        [SerializeField] private SpriteRenderer _mouthSpriteRenderer = null;

        public void SetCharacterSprites (Sprite backgroundSprite, Sprite faceShapeSprite, Sprite hairSprite, Sprite eyesSprite, Sprite noseSprite, Sprite mouthSprite)
        {
            _backgroundSpriteRenderer.sprite = backgroundSprite;
            _faceShapeSpriteRenderer.sprite = faceShapeSprite;
            _hairSpriteRenderer.sprite = hairSprite;
            _eyesSpriteRenderer.sprite = eyesSprite;
            _noseSpriteRenderer.sprite = noseSprite;
            _mouthSpriteRenderer.sprite = mouthSprite;
        }

    }
}