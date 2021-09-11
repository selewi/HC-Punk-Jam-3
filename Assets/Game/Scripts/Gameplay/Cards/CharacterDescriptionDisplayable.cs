using UnityEngine;
using TMPro;

namespace HCPJ3
{
    public class CharacterDescriptionDisplayable : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _nameTMP = null;
        [SerializeField] private TextMeshPro _descriptionTMP = null;

        public void SetName (string newText)
        {
            _nameTMP.text = newText;
        }

        public void SetDescription (string newText)
        {
            _descriptionTMP.text = newText;
        }
    }
}