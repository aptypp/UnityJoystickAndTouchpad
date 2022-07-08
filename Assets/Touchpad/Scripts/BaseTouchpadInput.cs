using UnityEngine;
using UnityEngine.EventSystems;

namespace Touchpad.Scripts
{
    public abstract class BaseTouchpadInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public virtual void OnPointerDown(PointerEventData eventData) {}

        public virtual void OnDrag(PointerEventData eventData) {}

        public virtual void OnPointerUp(PointerEventData eventData) {}
    }
}