using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.OnScreen {
    public class Joystick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [FormerlySerializedAs("movementRange")]
        [SerializeField]
        [Range(0f, 1000f)]
        private float handleLimit = 50;

        [Header("Components")]
        public RectTransform handle;

        [InputControl(layout = "Vector2")]
        [SerializeField]
        private string ControlPath;

        private Vector3 StartPosition;
        private Vector2 PointerDownPosition;

        protected override string controlPathInternal
        {
            get => ControlPath;
            set => ControlPath = value;
        }

        private void Start()
        {
            StartPosition = ((RectTransform)transform).anchoredPosition;
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData == null)
            {
                throw new System.ArgumentNullException(nameof(eventData));
            }
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(), eventData.position, eventData.pressEventCamera, out PointerDownPosition);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData == null)
                throw new System.ArgumentNullException(nameof(eventData));

            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(), eventData.position, eventData.pressEventCamera, out var position);
            var delta = position - PointerDownPosition;

            delta = Vector2.ClampMagnitude(delta, movementRange);
            handle.anchoredPosition = StartPosition + (Vector3)delta;

            var newPos = new Vector2(delta.x / movementRange, delta.y / movementRange);
            SendValueToControl(newPos);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            handle.anchoredPosition = StartPosition;
            SendValueToControl(Vector2.zero);
        }

        public float movementRange
        {
            get => handleLimit;
            set => handleLimit = value;
        }
    }
}


