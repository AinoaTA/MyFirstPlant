using UnityEngine;

namespace Custom
{ 
    public class CustomRotation : MonoBehaviour
    {
        [SerializeField] private float _rotSpeed = 400;
        private void OnMouseDrag()
        {
            transform.Rotate(0, (-Input.GetAxis("Mouse X") * _rotSpeed * Time.deltaTime), 0, Space.World);
        }
    }
}