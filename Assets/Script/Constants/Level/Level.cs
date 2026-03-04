using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Level", menuName = "Level")]
public class Level : ScriptableObject
{
    [SerializeField] private string _sceneName;
    [SerializeField] private AudioClip _soundtrack;
    [SerializeField] private ScreenId id;
    public string SceneName { get { return _sceneName; } }

    public void LoadLevel()
    {
        if (string.IsNullOrEmpty(_sceneName))
        {
            Debug.LogWarning($"[Level] El campo '_sceneName' en {name} está vacío.");
            return;
        }
        MainClass.AudioManager.SoundtrackManager.Play(_soundtrack);
        OnConfigInputSystemMap();
        SceneManager.LoadScene(_sceneName);
    }

    private void OnConfigInputSystemMap()
    {
        if (ScreenId.Menu == id)
        {
            InputManager.SwitchMap(InputManager.InputActions.UI);
        }
        else {
            InputManager.SwitchMap(InputManager.InputActions.Player);
        }
    }
}
