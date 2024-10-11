using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguagePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private List<Locale> _locales;
    private int _currentLocaleId;

    private void Awake()
    {
        var locales = LocalizationSettings.AvailableLocales;
        _locales = locales.Locales;
    }

    private void OnEnable()
    {
        var currentLocale = LocalizationSettings.SelectedLocale;
        _currentLocaleId = _locales.IndexOf(currentLocale);

        DisplayLocale();
    }

    public void OnChangeLanguage()
    {
        _currentLocaleId++;

        if(_currentLocaleId >= _locales.Count)
        {
            _currentLocaleId = 0;
        }

        DisplayLocale();
    }

    private void DisplayLocale()
    {
        _text.text = _locales[_currentLocaleId].LocaleName;

        LocalizationSettings.SelectedLocale = _locales[_currentLocaleId];
    }
}
