using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarouselDificultBt : MonoBehaviour
{
    private List<Dificult> options = new List<Dificult>();
    private int _index = 0;
    private TextMeshProUGUI Txt;
    protected void Awake()
    {
        options.Add(Dificult.Easy);
        options.Add(Dificult.Medium);
        options.Add(Dificult.Hard);
        Txt = GetComponentInChildren<TextMeshProUGUI>();
        MainClass.CustomEvents.OnLanguageChanged.AddListener(UpdateText);
    }

    private void Start()
    {
        UpdateText();
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