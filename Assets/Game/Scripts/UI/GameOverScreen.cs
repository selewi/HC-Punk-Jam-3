using HCPJ3.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace HCPJ3
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas;

        [SerializeField]
        private Button _restartButton;

        [SerializeField]
        private GameEvent _restartEvent;

        private void Awake()
        {
            _restartButton.onClick.AddListener(_restartEvent.Raise);
        }

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
