using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.tvOS;
using UnityEngineInternal;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private InputActionAsset controls;
    [SerializeField] private InputActionMap _inputActionMap;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private AnimationViewerManager animationViewerManager;

    public InputAction move_Action,
        look_Action,
        jump_Action,
        crouch_Action,
        sprint_Action,
        pause_Action,
        FireBall_Action;


    public Vector2 move, look;

    public bool jump, sprint, crouch, pause, shootingFireball;

    [Header("Mouse Settings")] public bool cursorLocked = false;
    public bool cursorInputForLook = true;

    private void OnEnable()
    {
        //Assign the inputactionmap to the player inputaction map that i have setup inside of my input action asset.
        _inputActionMap = controls.FindActionMap("Player");

        //Assign the actions to the action map actions
        move_Action = _inputActionMap.FindAction("Move");
        look_Action = _inputActionMap.FindAction("Look");
        jump_Action = _inputActionMap.FindAction("Jump");
        crouch_Action = _inputActionMap.FindAction("Crouch");
        sprint_Action = _inputActionMap.FindAction("Sprint");
        pause_Action = _inputActionMap.FindAction("Pause");
        FireBall_Action = _inputActionMap.FindAction("Fireball");
        move_Action.performed += OnMove;
        move_Action.canceled += OnEndMove;

        look_Action.performed += OnLook;
        look_Action.canceled += OnEndLook;

        pause_Action.started += OnPause;

        FireBall_Action.performed += OnFireBallShot;
        jump_Action.performed += OnJump;
        crouch_Action.performed += OnCrouch;
        sprint_Action.performed += OnSprint;

        FireBall_Action.canceled += OnEndFireBallShot;
        jump_Action.canceled += OnEndJump;
        crouch_Action.canceled += OnEndCrouch;
        sprint_Action.canceled += OnEndSprint;


        _playerInput.onControlsChanged += ControlsChanged;

        //Start the game originally with the mouse being locked.
        Cursor.lockState = CursorLockMode.Locked;
    }

    //when this object is disabled we will unsubscribe from the input events.
    private void OnDisable()
    {
        move_Action.performed -= OnMove;
        move_Action.canceled -= OnEndMove;

        look_Action.performed -= OnLook;
        look_Action.canceled -= OnEndLook;

        pause_Action.performed -= OnPause;

        FireBall_Action.performed -= OnFireBallShot;
        jump_Action.performed -= OnJump;
        crouch_Action.performed -= OnCrouch;
        sprint_Action.performed -= OnSprint;

        FireBall_Action.canceled -= OnEndFireBallShot;
        jump_Action.canceled -= OnEndJump;
        crouch_Action.canceled -= OnEndCrouch;
        sprint_Action.canceled -= OnEndSprint;

        _playerInput.onControlsChanged -= ControlsChanged;
    }

    private void ControlsChanged(PlayerInput playerInput)
    {
        if (playerInput.currentControlScheme.Equals("GamePad"))
        {
            Debug.Log("I am a gamepad.");
            _playerController.MouseSensitivity = _playerController.GamepadSensitivity;
        }
        else if (playerInput.currentControlScheme.Equals("Main"))
        {
            Debug.Log("I am a Keyboard and mouse.");
            _playerController.MouseSensitivity = _playerController.NormalMouseSensitivity;
        }
    }

    private void OnDeviceHaBeenChanged(InputUser iu, InputUserChange iuc, InputDevice id)
    {
        if (id.device.IsActuated() && id.device.Equals(Gamepad.current))
        {
            Debug.Log("i am currently a gamepad.");
        }
        else
        {
            Debug.Log("i am currently a keyboard.");
        }
    }

    private void Update()
    {
    }

    private void OnFireBallShot(InputAction.CallbackContext obj)
    {
        shootingFireball = true;
    }

    private void OnEndFireBallShot(InputAction.CallbackContext obj)
    {
        shootingFireball = false;
    }


    private void OnMove(InputAction.CallbackContext obj)
    {
        MoveInput(obj.action.ReadValue<Vector2>());
    }

    private void OnEndMove(InputAction.CallbackContext obj)
    {
        MoveInput(new Vector2(0, 0));
    }

    private void OnLook(InputAction.CallbackContext obj)
    {
        if (cursorInputForLook)
        {
            LookInput(obj.action.ReadValue<Vector2>());
        }
    }

    private void OnEndLook(InputAction.CallbackContext obj)
    {
        if (cursorInputForLook)
        {
            LookInput(new Vector2(0, 0));
        }
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        JumpInput(obj.action.IsPressed());
    }

    private void OnEndJump(InputAction.CallbackContext obj)
    {
        JumpInput(false);
    }

    private void OnSprint(InputAction.CallbackContext obj)
    {
        SprintInput(obj.action.IsPressed());
    }

    private void OnEndSprint(InputAction.CallbackContext obj)
    {
        SprintInput(false);
    }

    private void OnCrouch(InputAction.CallbackContext obj)
    {
        CrouchInput(obj.action.IsPressed());
    }

    private void OnEndCrouch(InputAction.CallbackContext obj)
    {
        CrouchInput(false);
    }

    private void OnPause(InputAction.CallbackContext obj)
    {
        animationViewerManager.StopPlayingGame();
    }

    private void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    private void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }


    private void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    private void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }

    private void CrouchInput(bool newCrouchState)
    {
        crouch = newCrouchState;
    }
}