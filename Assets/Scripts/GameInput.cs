using UnityEngine;
using System.Collections.Generic;
using System;


public class GameInput : MonoBehaviour
{
    public static GameInput Instance{get; private set;}

    private InputActions inputActions;
    public event EventHandler OnMenuButtonPressed;

    public void Awake()
    {
        inputActions = new InputActions();
        Instance = this;
        inputActions.Enable();
        inputActions.Player.Menu.performed += Menu_Performed;
    }
    private void OnDestroy()
    {
        inputActions.Disable();
    }

    private void Menu_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnMenuButtonPressed?.Invoke(this, EventArgs.Empty);
    }
    public bool IsUpActionPressed()
    {
        return inputActions.Player.LanderUp.IsPressed();
    }
    public bool IsLeftActionPressed()
    {
        return inputActions.Player.LanderLeft.IsPressed();
    }
    public bool IsRightActionPressed()
    {
        return inputActions.Player.LanderRight.IsPressed();
    }

    public Vector2 GetMovementInputVector2()
    {
        return inputActions.Player.Movement.ReadValue<Vector2>();
    }
}
