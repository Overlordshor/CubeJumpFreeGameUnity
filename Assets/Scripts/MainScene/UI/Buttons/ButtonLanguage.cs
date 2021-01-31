using UnityEngine;
using UnityEngine.UI;

public class ButtonLanguage : MonoBehaviour
{
    private Image _image;
    private string _language;

    [SerializeField] private Text _playGameText, _playReductionModeText, _record, _priceText, _exitText, _livesText;
    [SerializeField] private Sprite _eglish, _russian;

    public void GetLanguage()
    {
        if (_language == "English")
        {
            _image.sprite = _russian;
            _language = "Russian";
            PlayerPrefs.SetString(Keys.Language, "Russian");
        }
        else if (_language == "Russian")
        {
            _image.sprite = _eglish;
            _language = "English";
            PlayerPrefs.SetString(Keys.Language, "English");
        }

        ChangeLangueageOnScene();
    }

    /// <summary>
    /// For correct entry into the GetLanguage() function, you need to invert the language variable;
    /// </summary>
    private void SetLanguage()
    {
        if (PlayerPrefs.HasKey(Keys.Language))
        {
            _language = PlayerPrefs.GetString(Keys.Language) == "English" ? "Russian" : "English";
        }
        else
        {
            _language = "Russian"; // to invert the language
        }

        GetLanguage();
    }

    private void ChangeLangueageOnScene()
    {
        Language.PrintAnyLanguage(_playGameText,
            "TAP TO PLAY",
            "НАЖМИ ДЛЯ ИГРЫ");
        Language.PrintAnyLanguage(_playReductionModeText,
            "TAP TO PLAY",
            "НАЖМИ ДЛЯ ИГРЫ");
        Language.PrintAnyLanguage(_record,
           "TOP: " + PlayerPrefs.GetInt("Record").ToString(),
           "Рекорд: " + PlayerPrefs.GetInt("Record").ToString());
        Language.PrintAnyLanguage(_exitText,
            "Quit the game?",
            "Выйти из игры?");
        var countLives = _livesText.GetComponent<Lives>();
        countLives.Print();
    }

    private void Start()
    {
        _image = GetComponent<Image>();
        SetLanguage();
    }
}