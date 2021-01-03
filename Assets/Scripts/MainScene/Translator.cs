using UnityEngine;
using UnityEngine.UI;

public class Translator : MonoBehaviour
{
    [SerializeField] private string english;
    [SerializeField] private string russian;
    private Text text;

    private void OnEnable()
    {
        text = GetComponent<Text>();
        if (PlayerPrefs.HasKey(Keys.Language))
        {
            switch (PlayerPrefs.GetString(Keys.Language))
            {
                case "English":
                    text.text = english;
                    break;

                case "Russian":
                    text.text = russian;
                    break;
            }
        }
        else
        {
            text.text = english;
        }
    }
}