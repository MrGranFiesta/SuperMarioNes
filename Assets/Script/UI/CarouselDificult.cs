using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CarouselDificult : OptionBase
{
    private List<Dificult> options = new List<Dificult>();
    private int _index = 0;

    protected override void Awake()
    {
        base.Awake();
        options.Add(Dificult.Easy);
        options.Add(Dificult.Medium);
        options.Add(Dificult.Hard);
        MainClass.CustomEvents.OnLanguageChanged.AddListener(UpdateText);
    }

    void Start()
    {
        UpdateText();
    }

    public override void OnEnter()
    {
        NextOptions();
        SoundConst.Beep.Play();
        Txt.text = MainClass.I18n.Get(options[_index].InternationalizationKey);
        MainClass.Config.Dificult = options[_index];
    }

    public override void OnBack()
    {
        BackOptions();
        SoundConst.Beep.Play();
        Txt.text = MainClass.I18n.Get(options[_index].InternationalizationKey);
        MainClass.Config.Dificult = options[_index];
    }

    private void NextOptions()
    {
        if(_index < 2)
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

    void UpdateText()
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
