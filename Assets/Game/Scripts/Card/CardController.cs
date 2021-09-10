using UnityEngine;

namespace HCPJ3
{
    public class CardController : MonoBehaviour
    {
        private Camera _camera;
        private Vector3 _origin;

        private void Awake()
        {
            _camera = Camera.main;
            _origin = transform.position;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 position = _camera.ScreenToWorldPoint(Input.mousePosition);
                transform.position = position;
            }
            else
            {
                transform.position = _origin;
            }
        }
    }
}
