using UnityEngine;
using UnityEngine.EventSystems;

namespace Mal
{
    public class FixedJoystick : Joystick
    {
        Vector2 joystickPosition = Vector2.zero;
        private Camera cam = new Camera();

        void Start()
        {
            joystickPosition = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            Vector2 direction = eventData.position - joystickPosition;
            inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
            ClampJoystick();
            handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
            int clickID = eventData.pointerId;

            if (clickID == -1)
            {
                Debug.Log("Left mouse click registered");
            }
            else if (clickID == -2)
            {
                Debug.Log("Right mouse click registered");
            }
            else if (clickID == -3)
            {
                Debug.Log("Center mouse click registered");
            }
            else if (clickID == 0)
            {
                Debug.Log("Single tap registered");
            }
            else if (clickID == 1)
            {
                Debug.Log("Double tap registered");
            }
            else if (clickID == 2)
            {
                Debug.Log("Triple tap registered");
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            inputVector = Vector2.zero;
            handle.anchoredPosition = Vector2.zero;
        }
    }
}
