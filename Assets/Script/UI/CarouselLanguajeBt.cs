using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarouselLanguajeBt : MonoBehaviour
{
    private List<LanguajeCode> options = new List<LanguajeCode>();
    private int _index = 0;
    private TextMeshProUGUI Txt;

    protected void Awake()
    {
        Txt = GetComponentInChildren<TextMeshProUGUI>();
        options.Add(LanguajeCode.ES);
        options.Add(LanguajeCode.IN);

        MainClass.CustomEvents.OnLanguageChanged.AddListener(UpdateText);
    }

    public void NextOptions()
    {
        if (_index < options.Count - 1)
        {
            _index++;
        }
        else
        {
            _index = 0;
        }
        SoundConst.Beep.Play();
        Txt.text = MainClass.I18n.Get(options[_index].InternationalizationKey);
        MainClass.I18n.ChangeLanguage(options[_index].code);
        UpdateText();
    }

    private void UpdateText()
    {
        if (Txt == null) return;
        string translatedValue = MainClass.I18n.Get(options[_index].InternationalizationKey);
        Txt.text = translatedValue;
    }

    private void OnDestroy()
    {
        if (MainClass.CustomEvents != null)
        {
            MainClass.CustomEvents.OnLanguageChanged.RemoveListener(UpdateText);
        }
    }
}
