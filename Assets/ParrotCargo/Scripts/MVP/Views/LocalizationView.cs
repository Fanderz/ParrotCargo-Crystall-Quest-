using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LocalizationView : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _ruLangButton;
    [SerializeField] private Button _enLangButton;
    [SerializeField] private Button _trLangButton;
    [Header("Sprites")]
    [SerializeField] private Sprite _ruSelectedSprite;
    [SerializeField] private Sprite _enSelectedSprite;
    [SerializeField] private Sprite _trSelectedSprite;

    private Image _ruImage;
    private Image _enImage;
    private Image _trImage;

    private Sprite _ruDefaultSprite;
    private Sprite _enDefaultSprite;
    private Sprite _trDefaultSprite;

    private void Awake()
    {
        _ruImage = _ruLangButton.GetComponent<Image>();
        _enImage = _enLangButton.GetComponent<Image>();
        _trImage = _trLangButton.GetComponent<Image>();

        _ruDefaultSprite = _ruImage.sprite;
        _enDefaultSprite = _enImage.sprite;
        _trDefaultSprite = _trImage.sprite;
    }

    private void OnEnable()
    {
        SetSelectedLang(YG2.lang);

        _ruLangButton.onClick.AddListener(() => { YG2.SwitchLanguage("ru"); SetSelectedLang("ru"); });
        _enLangButton.onClick.AddListener(() => { YG2.SwitchLanguage("en"); SetSelectedLang("en"); });
        _trLangButton.onClick.AddListener(() => { YG2.SwitchLanguage("tr"); SetSelectedLang("tr"); });
    }

    private void OnDisable()
    {
        _ruLangButton.onClick.RemoveAllListeners();
        _enLangButton.onClick.RemoveAllListeners();
        _trLangButton.onClick.RemoveAllListeners();
    }

    private void SetSelectedLang(string lang)
    {
        ReturnToDefault();

        if (lang == "ru")
            _ruImage.sprite = _ruSelectedSprite;
        else if (lang == "en")
            _enImage.sprite = _enSelectedSprite;
        else if (lang == "tr")
            _trImage.sprite = _trSelectedSprite;
    }

    private void ReturnToDefault()
    {
        _ruImage.sprite = _ruDefaultSprite;
        _enImage.sprite = _enDefaultSprite;
        _trImage.sprite = _trDefaultSprite;
    }

    private void SetSelectedOnEnable()
    {
        if (YG2.lang == "ru")
            _ruLangButton.Select();
        else if (YG2.lang == "en")
            _enLangButton.Select();
        else if (YG2.lang == "tr")
            _trLangButton.Select();
    }
}
