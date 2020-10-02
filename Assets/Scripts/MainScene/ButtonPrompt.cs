using UnityEngine;
using UnityEngine.UI;

public class ButtonPrompt : MonoBehaviour
{
    public Sprite PromptOn, PromptOff;

    private Image image;
    private bool activePrompt = true;

    /// <summary>
    /// For correct entry into the GetPrompt() function, you need to invert the mute variable;
    /// </summary>
    private void SetPrompt()
    {
        if (PlayerPrefs.GetString("Prompt") == "True")
        {
            activePrompt = false;
        }
        else
        {
            activePrompt = true;
        }

        GetPrompt();
    }

    public void GetPrompt()
    {
        if (!activePrompt)
        {
            image.sprite = PromptOn;
            activePrompt = true;
            PlayerPrefs.SetString("Prompt", "True");
            print("Добавь подсказки");
        }
        else
        {
            image.sprite = PromptOff;
            activePrompt = false;
            PlayerPrefs.SetString("Prompt", "False");
            print("Убери подсказки");
        }
    }

    private void Start()
    {
        image = GetComponent<Image>();
        SetPrompt();
    }
}