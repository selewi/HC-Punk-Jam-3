using UnityEngine;
using UnityEngine.UI;

namespace HCPJ3
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas;

        public void Open()
        {
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
