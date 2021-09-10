using UnityEngine;

namespace HCPJ3
{
    public class CardController : MonoBehaviour
    {
        [SerializeField]
        private PortraitController _portrait;

        [SerializeField]
        private CharacterDescriptionController _description;

        public void Randomize(bool isDirtyCop)
        {
            _portrait.Randomize();
            _description.Randomize(isDirtyCop);
        }
    }
}
