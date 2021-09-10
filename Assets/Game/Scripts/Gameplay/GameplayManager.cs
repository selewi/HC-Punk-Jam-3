using System;
using UnityEngine;

namespace HCPJ3
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField]
        private CardManager _cardManager;

        [SerializeField]
        private ScoreManager _scoreManager;

        private void Awake()
        {
            _cardManager.Initialize();
            _scoreManager.Initialize();
        }

        private void Start ()
        {
            _cardManager.onCardRelease.AddListener (HandleCardManagerCardRelease);
        }

        private void OnDestroy ()
        {
            _cardManager.onCardRelease.RemoveListener (HandleCardManagerCardRelease);
        }

        private void HandleCardManagerCardRelease (CardManager.CardReleaseEventInfo eventInfo)
        {
            if (eventInfo.CardIsCop)
            {
                if (eventInfo.SwipeIsCorrect)
                {
                    _scoreManager.HandleCopFound ();
                } else
                {
                    _scoreManager.HandleCopMiss ();
                }
            } else
            {
                if (eventInfo.SwipeIsCorrect)
                {
                    _scoreManager.HandlePunkFound ();
                } else
                {
                    _scoreManager.HandlePunkMiss ();
                }
            }
        }
    }
}
