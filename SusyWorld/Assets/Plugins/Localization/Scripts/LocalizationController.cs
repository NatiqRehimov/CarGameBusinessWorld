using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
public class LocalizationController : MonoBehaviour
{
    public void OnLanguageDropdownPressed(int languageIndex)
    {
        Locale locale = LocalizationSettings.AvailableLocales.Locales[languageIndex];
        LocalizationSettings.Instance.SetSelectedLocale(locale);
    }
}
