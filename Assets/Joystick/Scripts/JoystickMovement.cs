using UnityEngine;

namespace AVP.Joystick.Scripts
{
    public class JoystickMovement : JoystickInput
    {
        private void Update()
        {
            if(!_isActive) return;
            
            GameMovement();
        }

        private void GameMovement() => Debug.Log($"Joystick direction: {_direction}");
    }
}