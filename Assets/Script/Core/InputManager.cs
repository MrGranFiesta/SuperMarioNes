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
                _inputActions.UI.Enable();
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
