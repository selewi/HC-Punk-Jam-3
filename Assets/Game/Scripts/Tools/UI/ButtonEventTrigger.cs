using UnityEngine;
using UnityEngine.UI;

namespace HCPJ3.Tools.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonEventTrigger : MonoBehaviour
    {
        [SerializeField]
        private GameEvent _event;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            Debug.Log("CLICKED");
            _event.Raise();
        }
    }
}
