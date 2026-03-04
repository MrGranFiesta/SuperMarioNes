using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CarouselLanguaje : OptionBase
{
    private List<LanguajeCode> options = new List<LanguajeCode>();
    private int _index = 0;

    protected override void Awake()
    {
        base.Awake();
        options.Add(LanguajeCode.ES);
        options.Add(LanguajeCode.IN);

        MainClass.CustomEvents.OnLanguageChanged.AddListener(UpdateText);
    }

    public override void OnEnter()
    {
        NextOptions();
        SoundConst.Beep.Play();
        Txt.text = MainClass.I18n.Get(options[_index].InternationalizationKey);
        MainClass.I18n.ChangeLanguage(options[_index].code);
    }

    public override void OnBack()
    {
        BackOptions();
        SoundConst.Beep.Play();
        Txt.text = MainClass.I18n.Get(options[_index].InternationalizationKey);
        MainClass.I18n.ChangeLanguage(options[_index].code);
    }

    private void NextOptions()
    {
        if (_index < 1)
        {
            _index++;
        }
    }

    private void BackOptions()
    {
        if (_index > 0)
        {
            _index--;
        }
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
