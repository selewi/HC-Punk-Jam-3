using NaughtyAttributes;
using UnityEngine;

namespace HCPJ3
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private GameOverScreen _gameOverScreen;

        public void Initialize()
        {
            _gameOverScreen.Close();
        }

        public void GameOver()
        {
            _gameOverScreen.Open();
        }

        #region Editor

        [Button("Toggle Game Over")]
        private void ToggleGameOver()
        {
            _gameOverScreen.Toggle();
        }

        #endregion Editor
    }
}
