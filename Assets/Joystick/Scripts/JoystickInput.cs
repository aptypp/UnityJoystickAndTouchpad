using UnityEngine;
using UnityEngine.EventSystems;

namespace AVP.Joystick.Scripts
{
    public abstract class JoystickInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private JoystickView _joystickView;

        [Header("Settings")]
        [SerializeField] private float _radius = 100.0f;

        protected bool _isActive;
        protected Vector2 _direction;

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            _isActive = true;
            _direction = eventData.position - (Vector2) _joystickView.getBackImageTransform.position;
            SetJoystickFrontImagePosition(eventData.position);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            Vector2 newDirection = Vector2.ClampMagnitude(eventData.position - (Vector2) _joystickView.getBackImageTransform.position, _radius);
            
            SetJoystickFrontImagePosition((Vector2)_joystickView.getBackImageTransform.position + newDirection);
            
            _direction = newDirection;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            _direction = Vector2.zero;
            SetJoystickFrontImagePosition(_joystickView.getBackImageTransform.position);
            _isActive = false;
        }

        private void SetJoystickFrontImagePosition(Vector2 position) => _joystickView.getFrontImageTransform.position = position;
    }
}