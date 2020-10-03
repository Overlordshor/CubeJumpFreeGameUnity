using UnityEngine;
using UnityEngine.UI;

public class Language
{
    public static void PrintAnyLanguage(Text text, string english, string russian)
    {
        switch (PlayerPrefs.GetString("Language"))
        {
            case "English":
                text.text = english;
                break;

            case "Russian":
                text.text = russian;
                break;
        }
    }
}