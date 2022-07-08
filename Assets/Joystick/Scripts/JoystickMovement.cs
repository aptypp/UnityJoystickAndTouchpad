using UnityEngine;
using UnityEngine.EventSystems;

namespace AVP.Joystick.Scripts
{
    public class JoystickMovement : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public Vector2 getDirection => _direction;
        
        [SerializeField] private JoystickView _joystickView;

        [Header("Settings")]
        [SerializeField] private float _radius = 100.0f;
        
        private Vector2 _direction = Vector2.zero;
         
        public void OnPointerDown(PointerEventData eventData)
        {
            _direction = eventData.position - (Vector2) _joystickView.getBackImageTransform.position;
            SetJoystickFrontImagePosition(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 newDirection = Vector2.ClampMagnitude(eventData.position - (Vector2) _joystickView.getBackImageTransform.position, _radius);
            
            SetJoystickFrontImagePosition((Vector2)_joystickView.getBackImageTransform.position + newDirection);
            
            _direction = newDirection;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _direction = Vector2.zero;
            SetJoystickFrontImagePosition(_joystickView.getBackImageTransform.position);
        }

        private void SetJoystickFrontImagePosition(Vector2 position) => _joystickView.getFrontImageTransform.position = position;
    }
}