using UnityEngine;
using UnityEngine.UI;

public class ButtonLanguage : MonoBehaviour
{
    private Image _image;
    private string _language;

    [SerializeField]
    private GameEvent _onSelected;

    public Sprite English, Russian;
    public Text PlayGameText, PlayReductionModeText, Record, PriceText, ExitText;

    public void GetLanguage()
    {
        if (_language == "English")
        {
            _image.sprite = Russian;
            _language = "Russian";
            PlayerPrefs.SetString(Keys.Language, "Russian");
        }
        else if (_language == "Russian")
        {
            _image.sprite = English;
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
        Language.PrintAnyLanguage(PlayGameText,
            "TAP TO PLAY",
            "НАЖМИ ДЛЯ ИГРЫ");
        Language.PrintAnyLanguage(PlayReductionModeText,
            "TAP TO PLAY",
            "НАЖМИ ДЛЯ ИГРЫ");
        Language.PrintAnyLanguage(Record,
           "TOP: " + PlayerPrefs.GetInt("Record").ToString(),
           "Рекорд: " + PlayerPrefs.GetInt("Record").ToString());
        Language.PrintAnyLanguage(ExitText,
            "Quit the game?",
            "Выйти из игры?");

        // new translation
        _onSelected.Raise();
    }

    private void Start()
    {
        _image = GetComponent<Image>();
        SetLanguage();
    }
}