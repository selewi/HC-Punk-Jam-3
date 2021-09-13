using UnityEngine;

namespace HCPJ3
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private float _rotationOnClick = 10;

        [SerializeField]
        private float _rotationSpeed = 10;

        [SerializeField]
        private Texture2D _systemCursorTexture;

        private float _initialRotation;

        private void Awake()
        {
            Cursor.visible = false;

#if UNITY_WEBGL
            Cursor.SetCursor(_systemCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
#endif

            Cursor.lockState = CursorLockMode.Confined;
            _initialRotation = transform.eulerAngles.z;
        }

        private void Update()
        {
            Vector3 position = _camera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(position.x, position.y);
            
            float rotation = Input.GetMouseButton(0) ? _initialRotation + _rotationOnClick : _initialRotation;
            rotation = Mathf.Lerp(transform.eulerAngles.z, rotation, Time.deltaTime * _rotationSpeed);
            transform.eulerAngles = new Vector3(0, 0, rotation);
        }
    }
}
