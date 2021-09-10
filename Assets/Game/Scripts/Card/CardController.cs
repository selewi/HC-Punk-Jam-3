using UnityEngine;

namespace HCPJ3
{
    public class CardController : MonoBehaviour
    {
        private Card _card;

        public void Initialize(Card card)
        {
            _card = card;
            Refresh();
        }

        private void Refresh()
        {
        }
    }
}
