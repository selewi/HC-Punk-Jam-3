using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

namespace HCPJ3
{
    public class CardController : MonoBehaviour
    {
        [SerializeField]
        private PortraitController _portrait;

        [SerializeField]
        private CharacterDescriptionController _description;

        [SerializeField]
        private CardOutline _outline;

        [SerializeField]
        private SwipeTextController _swipeTextController;

        private SortingGroup _sortingGroup;
        private readonly List<Tween> _tweens = new List<Tween>();

        public bool IsCop { get; private set; }

        public CardOutline Outline => _outline;
        public SwipeTextController SwipeTextController => _swipeTextController;

        private void Awake ()
        {
            TryGetComponent (out _sortingGroup);
            Outline.SetVisible(false);
        }

        public void AnimateOnRelease(bool transition)
        {
            Outline.SetDirection(CardManager.Direction.None);
            Outline.SetVisible(false);

            CompleteAllTweens();

            if (transition)
            {
                 _tweens.Add(transform.DOScale (Vector3.one, 0.1f));
            } else
            {
                transform.localScale = Vector3.one;
            }

            _tweens.Add(transform.DOLocalMove(Vector3.zero, 0.25f));
            _tweens.Add(transform.DOLocalRotate(Vector3.zero, 0.25f));
        }

        public void AnimateOnPick()
        {
            CompleteAllTweens();
            _tweens.Add(transform.DOScale(new Vector3(1.1f, 1.1f, 1), 0.25f));
        }

        private void CompleteAllTweens()
        {
            foreach (Tween tween in _tweens)
            {
                tween.Complete();
            }

            _tweens.Clear();
        }

        public void Randomize(bool isDirtyCop)
        {
            IsCop = isDirtyCop;
            _portrait.Randomize(isDirtyCop);
            _description.Randomize(isDirtyCop);
            _outline.SetDirection(CardManager.Direction.None);
            _swipeTextController.SetVisibility (0);
        }

        public void SetSortingOrder (int newSortingOrder)
        {
            _sortingGroup.sortingOrder = newSortingOrder;
        }
    }
}
