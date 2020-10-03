using UnityEngine;
using UnityEngine.UI;

public class Language
{
    /// <summary>
    /// In the presented text on the stage, chooses which text to submit depending on the player's settings.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="english"></param>
    /// <param name="russian"></param>
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