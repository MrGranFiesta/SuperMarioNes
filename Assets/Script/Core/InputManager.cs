using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    private static PlayerInputAction _inputActions;
    public static PlayerInputAction InputActions
    {
        get
        {
            if (_inputActions == null)
            {
                _inputActions = new PlayerInputAction();
                _inputActions.Player.Enable();
            }

            return _inputActions;
        }
    }

    public static void SwitchMap(InputActionMap mapToActivate)
    {
        InputActions.Disable();
        mapToActivate.Enable();
    }
}
