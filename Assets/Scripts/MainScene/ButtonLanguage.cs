using UnityEngine;
using UnityEngine.UI;

public class ButtonLanguage : MonoBehaviour
{
    public Sprite English, Russian;
    public Text PlayGameText, Record;

    private Image image;
    private string language;

    public void GetLanguage()
    {
        if (language == "Russian")
        {
            image.sprite = English;
            language = "English";
            PlayerPrefs.SetString("Language", "English");
        }
        else
        {
            image.sprite = Russian;
            language = "Russian";
            PlayerPrefs.SetString("Language", "Russian");
        }
        ChangeLangueageOnScene();
    }

    /// <summary>
    /// For correct entry into the GetLanguage() function, you need to invert the language variable;
    /// </summary>
    private void SetLanguage()
    {
        language = PlayerPrefs.GetString("Language") == "English" ? "Russian" : "English";

        GetLanguage();
    }

    private void ChangeLangueageOnScene()
    {
        Language.PrintAnyLanguage(PlayGameText,
            "TAP TO PLAY",
            "НАЖМИ ДЛЯ ИГРЫ");
        Language.PrintAnyLanguage(Record,
           "TOP: " + PlayerPrefs.GetInt("Record").ToString(),
           "Рекорд: " + PlayerPrefs.GetInt("Record").ToString());
    }

    private void Start()
    {
        image = GetComponent<Image>();
        SetLanguage();
    }
}