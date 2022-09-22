using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInput playerInput;

    [Header("Character Input Values")]
    public Vector2 move;
    public bool sprint;
    public bool jump;

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
        if (playerInput ==  null)
        {
            playerInput = new PlayerInput();

            playerInput.PlayerMovement.Movement.started += onMoveInput;
            playerInput.PlayerMovement.Movement.performed += onMoveInput;
            playerInput.PlayerMovement.Movement.canceled += onMoveInput;
            playerInput.PlayerMovement.Sprint.performed += onSprint;
            playerInput.PlayerMovement.File.performed += OpenFile;
            playerInput.PlayerMovement.DebugAdd.performed += onDebugAdd;
            playerInput.PlayerMovement.DebugSub.performed += onDebugSub;
            playerInput.PlayerMovement.Look.performed += onLook;
            playerInput.PlayerMovement.StartLoad.performed += onload;
            playerInput.PlayerMovement.Mouse.performed += onMouse;

            playerInput.PlayerMovement.Jump.started += onJump;
            playerInput.PlayerMovement.Jump.performed += onJump;
            playerInput.PlayerMovement.Jump.canceled += onJump;
        }
        playerInput.Enable();
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
        HandleMoventInput();
    }
    private void onLook(InputAction.CallbackContext i)
    {
        _look = i.ReadValue<Vector2>();
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

    private void HandleMoventInput()
    {
        verticalInput = move.y;
        horizontalInput = move.x;
    }
}
