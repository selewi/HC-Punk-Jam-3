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

        private SortingGroup _sortingGroup;

        public bool IsCop { get; private set; }

        private void Awake ()
        {
            TryGetComponent (out _sortingGroup);
        }

        public void Randomize(bool isDirtyCop)
        {
            IsCop = isDirtyCop;
            _portrait.Randomize();
            _description.Randomize(isDirtyCop);
        }

        public void SetSortingOrder (int newSortingOrder)
        {
            _sortingGroup.sortingOrder = newSortingOrder;
        }
    }
}
