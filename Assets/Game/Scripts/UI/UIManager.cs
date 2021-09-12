using NaughtyAttributes;
using UnityEngine;

namespace HCPJ3
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private GameOverScreen _gameOverScreen;

        #region Editor

        [Button("Toggle Game Over")]
        private void ToggleGameOver()
        {
            _gameOverScreen.Toggle();
        }

        #endregion Editor
    }
}
