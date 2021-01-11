using UnityEngine;
using UnityEngine.UI;

public class Translator : MonoBehaviour
{
    [SerializeField] private string english;
    [SerializeField] private string russian;
    private Text _text;

    private void OnEnable()
    {
        _text = GetComponent<Text>();
        TranslateText();
    }

    private void TranslateText()
    {
        if (PlayerPrefs.HasKey(Keys.Language))
        {
            switch (PlayerPrefs.GetString(Keys.Language))
            {
                case "English":
                    _text.text = english;
                    break;

                case "Russian":
                    _text.text = russian;
                    break;
            }
        }
        else
        {
            _text.text = english;
        }
    }

    public void UpdateLanguageText()
    {
        TranslateText();
    }
}