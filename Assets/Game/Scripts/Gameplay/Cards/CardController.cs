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

        public bool IsCop { get; private set; }

        public CardOutline Outline => _outline;
        public SwipeTextController SwipeTextController => _swipeTextController;

        private void Awake ()
        {
            TryGetComponent (out _sortingGroup);
            Outline.SetVisible(false);
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
