using System;
using UnityEngine;

namespace HCPJ3
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField]
        private CardManager _cardManager;

        private void Awake()
        {
            _cardManager.Initialize();
        }
    }
}
