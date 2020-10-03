using UnityEngine;
using UnityEngine.UI;

public class ButtonLanguage : MonoBehaviour
{
    public Sprite English, Russian;

    private Image image;
    private string language;

    /// <summary>
    /// For correct entry into the GetLanguage() function, you need to invert the language variable;
    /// </summary>
    private void SetLanguage()
    {
        language = PlayerPrefs.GetString("Language") == "English" ? "Russian" : "English";

        GetLanguage();
    }

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
    }

    private void Start()
    {
        image = GetComponent<Image>();
        SetLanguage();
    }
}