using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PauseManager : MonoBehaviour
{
    private bool _isPaused = false;
    private TextMeshProUGUI _txt;
    public void Awake()
    {
        _txt = GetComponent<TextMeshProUGUI>();
        InputManager.InputActions.Player.Pause.performed += HandlePause;
    }

    private void HandlePause(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _isPaused = !_isPaused;
        SoundConst.Pause.Play();
        if (_isPaused)
        {
            _txt.text = MainClass.I18n.Get("_pause");
            Time.timeScale = 0f;
        }
        else
        {
            _txt.text = "";
            Time.timeScale = 1f;
        }
    }
}
