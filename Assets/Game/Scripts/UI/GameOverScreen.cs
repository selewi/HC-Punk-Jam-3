using DG.Tweening;
using UnityEngine;

namespace HCPJ3
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas;

        public void Open()
        {
            transform.localScale = Vector3.one * 50;
            transform.DOScale(Vector3.one, 0.3f);
            _canvas.enabled = true;
        }

        public void Close()
        {
            _canvas.enabled = false;
        }

        public void Toggle()
        {
            _canvas.enabled = !_canvas.enabled;
        }
    }
}
