using UnityEngine;
using TMPro;

namespace HCPJ3
{
    public class CharacterDescriptionDisplayable : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _descriptionTMP = null;

        public void SetText (string newText)
        {
            _descriptionTMP.text = newText;
        }
    }
}