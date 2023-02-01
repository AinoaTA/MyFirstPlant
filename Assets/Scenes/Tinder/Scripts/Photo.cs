using UnityEngine;

namespace Tinder
{
    public class Photo : MonoBehaviour
    {
        Camera _cam;
        Vector3 _mousePos;
        [SerializeField] private float _maxRot = 30;
        [SerializeField] private float _maxUIRot = 15;
        [SerializeField] private float _maxUIRotScroll = 7.5f;
        [SerializeField] private float _maxXValue = 0.5f;
        [SerializeField] private float _offSet = 0.1f;
        private float _currentXValue;
        private float _currentZUIValue;
        private float _currentZUIValueScroll;
        [SerializeField] bool _deny, _accept;

        [HideInInspector] public SpriteRenderer sp;
        [SerializeField] private Transform _name, _zodiacSign, _scroll;

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
            _currentZUIValue = _mousePos.x * _maxUIRot / _maxXValue;
            _currentZUIValueScroll = _mousePos.x * _maxUIRotScroll / _maxXValue;

            transform.localRotation = Quaternion.Euler(Mathf.Clamp(_currentXValue, -_maxRot, _maxRot), 0, 0);

            _scroll.localRotation = Quaternion.Euler(0, 0, -Mathf.Clamp(_currentZUIValueScroll, -_maxUIRotScroll, _maxUIRotScroll));

            Quaternion newUIRot = Quaternion.Euler(0, 0, -Mathf.Clamp(_currentZUIValue, -_maxUIRot, _maxUIRot));
            _name.localRotation = newUIRot;
            _zodiacSign.localRotation = newUIRot;

            if (_currentXValue <= -_maxRot + _offSet) _deny = true;
            else _deny = false;

            if (_currentXValue >= _maxRot - _offSet) _accept = true;
            else _accept = false;
        }

        private void OnMouseUp()
        {
            if (_deny)
                Controller.controller.Deny();
            else if (_accept)
                Controller.controller.Accept();

            transform.localRotation = Quaternion.identity;
            _name.localRotation = Quaternion.identity;
            _zodiacSign.localRotation = Quaternion.identity;
            _scroll.localRotation = Quaternion.identity;
        }
    }
}