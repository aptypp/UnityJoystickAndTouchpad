using UnityEngine;
using UnityEngine.EventSystems;

namespace AVP.Joystick.Scripts
{
    public abstract class JoystickInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [Header("References")]
        [SerializeField] private JoystickView _joystickView;

        [Header("Settings")]
        [SerializeField] private float _referenceRadius = 100.0f;
        [SerializeField] private Vector2 _referenceResolution = new (1920, 1080);

        protected bool _isActive;
        protected Vector2 _direction;
        
        private float _radius;
        private float _inverseRadius;

        private void Awake() => InitFields();
        
#if UNITY_EDITOR
        private void OnValidate() => InitFields();
#endif
        
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            _isActive = true;
            _direction = (eventData.position - (Vector2) _joystickView.getBackImageTransform.position).normalized;
            SetJoystickFrontImagePosition(eventData.position);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            Vector2 newDirection = Vector2.ClampMagnitude(eventData.position - (Vector2) _joystickView.getBackImageTransform.position, _radius);
            
            SetJoystickFrontImagePosition((Vector2)_joystickView.getBackImageTransform.position + newDirection);
            
            _direction = newDirection * _inverseRadius;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            _direction = Vector2.zero;
            SetJoystickFrontImagePosition(_joystickView.getBackImageTransform.position);
            _isActive = false;
        }

        private void SetJoystickFrontImagePosition(Vector2 position) => _joystickView.getFrontImageTransform.position = position;

        private void InitFields()
        {
            _radius = _referenceRadius * Screen.width / _referenceResolution.x;
            _inverseRadius = 1.0f / _radius;
        }
    }
}