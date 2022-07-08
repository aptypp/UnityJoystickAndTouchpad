using UnityEngine;
using UnityEngine.UI;

namespace AVP.Joystick.Scripts
{
    public class JoystickView : MonoBehaviour
    {
        public Transform getFrontImageTransform => _frontImageTransform;
        public Transform getBackImageTransform => _backImageTransform;
        
        [SerializeField] private Image _frontImage;
        [SerializeField] private Image _backImage;

        private Transform _frontImageTransform;
        private Transform _backImageTransform;

        private void Awake()
        {
            _frontImageTransform = _frontImage.transform;
            _backImageTransform = _backImage.transform;
        }
    }
}