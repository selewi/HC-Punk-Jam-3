using UnityEngine;

namespace HCPJ3
{
    public class CardController : MonoBehaviour
    {
        [SerializeField]
        private PortraitController _portrait;

        [SerializeField]
        private CharacterDescriptionController _description;

        public bool IsCop { get; private set; }

        public void Randomize(bool isDirtyCop)
        {
            IsCop = isDirtyCop;
            _portrait.Randomize();
            _description.Randomize(isDirtyCop);
        }
    }
}
