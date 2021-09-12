using DG.Tweening;
using TMPro;
using UnityEngine;

namespace HCPJ3
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _TMP;

        private void Start()
        {
            _TMP.transform.DOPunchScale(new Vector3(0.05f, 0.05f, 0), 1, 5).SetLoops(-1);
        }
    }
}
