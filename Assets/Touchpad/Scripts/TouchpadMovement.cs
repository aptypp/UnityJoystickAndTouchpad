using UnityEngine;
using UnityEngine.EventSystems;

namespace Touchpad.Scripts
{
    public class TouchpadMovement : BaseTouchpadInput
    {
        public override void OnDrag(PointerEventData eventData) => GameMovement(eventData.delta);
        
        private void GameMovement(Vector2 deltaPosition) => Debug.Log($"Touch delta position: {deltaPosition}");
    }
}