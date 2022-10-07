using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mal
{
    public class InputManager : MonoBehaviour
    {

        private static InputManager Instance;
        public static InputManager myInstance {
            get
            {
                if (Instance == null)
                {
                    Instance = FindObjectOfType<InputManager>();
                }
                return Instance;
            }
        }

        public PlayerInput playerInput;

        [Header("Character Input Values")]
        public Vector2 move;
        public bool sprint;
        public bool jump;
        public bool attack;

        public bool ptouch;
        public bool touchZero;

        public float verticalInput;
        public float horizontalInput;
        public bool openFile;
        public bool start;
        public bool add;
        public bool sub;
        public bool mouseclick;
        [Header("Camera Input Values")]
        public Vector2 _look;
        internal int cameraInputX;
        internal int cameraInputY;


        private void OnEnable()
        {
            if (playerInput == null)
            {
                playerInput = new PlayerInput();

                playerInput.PlayerMovement.Movement.started += onMoveInput;
                playerInput.PlayerMovement.Movement.performed += onMoveInput;
                playerInput.PlayerMovement.Movement.canceled += onMoveInput;
                playerInput.PlayerMovement.Sprint.performed += onSprint;
                playerInput.PlayerMovement.File.performed += OpenFile;
                playerInput.PlayerMovement.DebugAdd.performed += onDebugAdd;
                playerInput.PlayerMovement.DebugSub.performed += onDebugSub;

                playerInput.PlayerMovement.Look.started += onLook;
                playerInput.PlayerMovement.Look.performed += onLook;
                playerInput.PlayerMovement.Look.canceled += onLook;

                playerInput.PlayerTouch.Touch.started += onTouch;
                playerInput.PlayerTouch.Touch.performed += onTouch;
                playerInput.PlayerTouch.Touch.canceled += onTouch;

                playerInput.PlayerTouch.Touch0.started += onTouchZero;
                playerInput.PlayerTouch.Touch0.performed += onTouchZero;
                playerInput.PlayerTouch.Touch0.canceled += onTouchZero;

                playerInput.PlayerMovement.StartLoad.performed += onload;
                playerInput.PlayerMovement.Mouse.performed += onMouse;

                playerInput.PlayerMovement.Jump.started += onJump;
                playerInput.PlayerMovement.Jump.performed += onJump;
                playerInput.PlayerMovement.Jump.canceled += onJump;

                playerInput.PlayerMovement.Attack.started += onAttack;
                playerInput.PlayerMovement.Attack.performed += onAttack;
                playerInput.PlayerMovement.Attack.canceled += onAttack;
            }
            playerInput.Enable();
        }
        private void onAttack(InputAction.CallbackContext i)
        {
            attack = i.ReadValueAsButton();
        }
        private void onJump(InputAction.CallbackContext i)
        {
            jump = i.ReadValueAsButton();
        }
        private void OpenFile(InputAction.CallbackContext i)
        {
            openFile = i.ReadValueAsButton();
        }
        private void onMouse(InputAction.CallbackContext i)
        {
            mouseclick = i.ReadValueAsButton();
        }
        private void onload(InputAction.CallbackContext i)
        {
            start = i.ReadValueAsButton();
        }

        private void onMoveInput(InputAction.CallbackContext i)
        {
            move = i.ReadValue<Vector2>();
        }
        private void onLook(InputAction.CallbackContext i)
        {
            _look = i.ReadValue<Vector2>();
        }

        private void onTouch(InputAction.CallbackContext i)
        {
            ptouch = i.ReadValueAsButton();
        }
        private void onTouchZero(InputAction.CallbackContext i)
        {
            touchZero = i.ReadValueAsButton();
        }

        private void onSprint(InputAction.CallbackContext i)
        {
            sprint = i.ReadValueAsButton();
        }
        private void onDebugAdd(InputAction.CallbackContext i)
        {
            add = i.ReadValueAsButton();
        }
        private void onDebugSub(InputAction.CallbackContext i)
        {
            sub = i.ReadValueAsButton();
        }
        private void OnDisable()
        {
            playerInput.Disable();
        }
    }
}

