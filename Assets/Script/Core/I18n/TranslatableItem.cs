using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TranslatableItem : MonoBehaviour
{
    private TextMeshProUGUI _text;
    [SerializeField] private string _key;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();

        // Nos suscribimos al evento de cambio de idioma.
        // Al usar UnityEvents, cada vez que se invoque 'OnLanguageChanged', 
        // este objeto ejecutará UpdateText().
        MainClass.CustomEvents.OnLanguageChanged.AddListener(UpdateText);
    }


    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    void UpdateText()
    {
        if (_text == null) return;
        string translatedValue = MainClass.I18n.Get(this._key);
        _text.text = translatedValue;
    }

    private void OnDestroy()
    {
        if (MainClass.CustomEvents != null)
        {
            MainClass.CustomEvents.OnLanguageChanged.RemoveListener(UpdateText);
        }
    }
}
