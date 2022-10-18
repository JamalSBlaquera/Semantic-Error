using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Mal
{
    public class CameraManager : MonoBehaviour, IPointerDownHandler

    {
        InputManager inputManager;
        FixedJoystick fixedJoystick;
        public Transform targetObject;
        public Vector3 cameraVelocity = Vector3.zero;
        public float cameraSpeed = 0.2f;
        public float cameraLookSpeed = .5f;
        public float cameraPivoteSpeed = 2;
        public float minimumPivotAngle = -35;
        public float maximumPivotAngle = 35;
        public Transform cameraPivote;
        [SerializeField]
        public TMP_Text id;

        public float lookAngle;
        public float pivotAngle;

        private void Awake()
        {
            inputManager = FindObjectOfType<InputManager>();
            targetObject = FindObjectOfType<Player>().transform;
            fixedJoystick = FindObjectOfType<FixedJoystick>();
            id = GetComponent<TMP_Text>();
        }

        public void HandleAllCameraMovement()
        {
            FollowTarget();
            CameraRotation();
        }

        private void FollowTarget()
        {
            Vector3 targetPosition = Vector3.SmoothDamp
                (transform.position, targetObject.position, ref cameraVelocity, cameraSpeed);

            transform.position = targetPosition;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
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
        private void CameraRotation()
        {
            Vector3 rotation;
            Quaternion targetRotation;

            lookAngle = lookAngle + (inputManager._look.x * cameraLookSpeed);
            pivotAngle = pivotAngle - (inputManager._look.y * cameraPivoteSpeed);
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

            rotation = Vector3.zero;
            rotation.y = lookAngle;
            targetRotation = Quaternion.Euler(rotation);
            transform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;
            targetRotation = Quaternion.Euler(rotation);
            cameraPivote.localRotation = targetRotation;
        }
    }
}

