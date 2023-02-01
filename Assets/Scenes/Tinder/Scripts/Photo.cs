using UnityEngine;

namespace Tinder
{
    public class Photo : MonoBehaviour
    {
        Camera _cam;
        Vector3 _mousePos;
        [SerializeField] private float _maxRot = 30;
        [SerializeField] private float _maxXValue = 0.5f;
        [SerializeField] private float _offSet = 0.1f;
        private float _currentXValue;

        [SerializeField] bool _deny, _accept;

        [HideInInspector] public SpriteRenderer sp;
        private void Awake()
        {
            transform.GetChild(0).TryGetComponent(out sp);
        }

        private void Start()
        {
            _cam = Controller.controller.cam;
        }

        private void OnMouseDrag()
        {
            if (Controller.controller.adverMinMatches.activeSelf) return;

            _mousePos = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cam.nearClipPlane));
            _currentXValue = _mousePos.x * _maxRot / _maxXValue;

            transform.localRotation = Quaternion.Euler(Mathf.Clamp(_currentXValue, -_maxRot, _maxRot), 0, 0);

            if (_currentXValue <= -(_maxRot + _offSet)) _deny = true;
            else _deny = false;

            if (_currentXValue >= (_maxRot - _offSet)) _accept = true;
            else _accept = false;
        }

        private void OnMouseUp()
        {
            if (_deny)
                Controller.controller.Deny();
            else if (_accept)
                Controller.controller.Accept();


            transform.localRotation = Quaternion.identity;
        }
    }
}