using System;
using System.Collections.Generic;
using UnityEngine;

public class I18n
{
    private Dictionary<string, string> _translationsDictionary = new Dictionary<string, string>();

    public I18n(string language = "")
    {
        if (string.IsNullOrEmpty(language))
        {
            language = LanguajeCode.ES.code;
        }
        ChangeLanguage(language);
    }

    public void ChangeLanguage(string language)
    {
        TextAsset contentJson = Resources.Load<TextAsset>($"i18n/{language}");

        // Si el archivo no existe, evitamos el crash
        if (contentJson == null)
        {
            Debug.LogError($"[i18n] No se encontró el archivo de idioma: Resources/i18n/{language}");
            return;
        }

        try
        {
            TranslationsDTO translationsDTO = JsonUtility.FromJson<TranslationsDTO>(contentJson.text);
            if (translationsDTO != null && translationsDTO.Translations != null)
            {
                _translationsDictionary = ConvertToDictionary(translationsDTO.Translations);
                MainClass.CustomEvents.OnLanguageChanged?.Invoke();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[i18n] Error al parsear el JSON de idioma: {e.Message}");
        }
    }

    private Dictionary<string, string> ConvertToDictionary(List<TranslateItem> translations)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        foreach (var translation in translations)
        {
            // TryAdd evita errores de claves duplicadas en el JSON
            if (!result.TryAdd(translation.Key, translation.Value))
            {
                Debug.LogWarning($"[i18n] Clave duplicada detectada y omitida: {translation.Key}");
            }
        }

        return result;
    }

    public string Get(string key)
    {
        if (string.IsNullOrEmpty(key)) return string.Empty;

        if (!_translationsDictionary.TryGetValue(key, out string value))
        {
            return key;
        }
        return value;
    }
}
