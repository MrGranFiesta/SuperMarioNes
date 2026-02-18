using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChangeLanguage : MonoBehaviour
{
    private Button _button;
    [SerializeField] private string _language;

    private void Awake()
    {
        _button = GetComponent<Button>();

        if (_button != null)
        {
            _button.onClick.AddListener(MakeChange);
        }
    }

    private void MakeChange()
    {
        if (string.IsNullOrEmpty(_language))
        {
            Debug.LogWarning($"[ChangeLanguage] El campo '_language' en {gameObject.name} está vacío.");
            return;
        }

        MainClass.I18n.ChangeLanguage(_language);
    }
}
