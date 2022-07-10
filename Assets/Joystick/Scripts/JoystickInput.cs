using System.Collections;
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
        [SerializeField] private float _returningSpeed = 10.0f;

        protected bool _isActive;
        protected Vector2 _direction;
        
        private bool _isReturning;
        private float _radius;
        private float _inverseRadius;
        private Coroutine _returningRoutine;
        
        private void Awake() => InitFields();
        
#if UNITY_EDITOR
        private void OnValidate() => InitFields();
#endif
        
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (_isActive) return;
            
            if (_isReturning) CancelReturnFrontImage();
            
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
            if (!_isActive) return; 
            
            _direction = Vector2.zero;
            ReturnFrontImage();
            _isActive = false;
        }

        private void SetJoystickFrontImagePosition(Vector2 position) => _joystickView.getFrontImageTransform.position = position;

        private void InitFields()
        {
            _radius = _referenceRadius * Screen.width / _referenceResolution.x;
            _inverseRadius = 1.0f / _radius;
        }

        private void CancelReturnFrontImage()
        {
            StopCoroutine(_returningRoutine);
            _isReturning = false;
        }
        
        private void ReturnFrontImage() => _returningRoutine = StartCoroutine(ReturnFrontImageRoutine());

        private IEnumerator ReturnFrontImageRoutine()
        {
            _isReturning = true;
            WaitForEndOfFrame delay = new WaitForEndOfFrame();

            float step = 1.0f / 100.0f;
            Vector2 targetPosition = _joystickView.getBackImageTransform.position;
            Vector2 startPosition = _joystickView.getFrontImageTransform.position;

            for (float i = 0; i < 1.0f; i += step * Time.deltaTime)
            {
                SetJoystickFrontImagePosition(Vector2.Lerp(startPosition, targetPosition, i * _returningSpeed));
                yield return delay;
            }
            
            SetJoystickFrontImagePosition(targetPosition);

            _isReturning = false;
        }
    }
}