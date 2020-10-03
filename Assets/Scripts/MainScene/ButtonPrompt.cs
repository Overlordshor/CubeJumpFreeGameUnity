using UnityEngine;
using UnityEngine.UI;

public class ButtonPrompt : MonoBehaviour
{
    public Sprite PromptOn, PromptOff;

    private Image image;
    private bool activePrompt = true;

    /// <summary>
    /// For correct entry into the GetPrompt() function, you need to invert the activePrompt variable;
    /// </summary>
    private void SetPrompt()
    {
        activePrompt = PlayerPrefs.GetString("Prompt") != "True";

        GetPrompt();
    }

    public void GetPrompt()
    {
        if (!activePrompt)
        {
            image.sprite = PromptOn;
            activePrompt = true;
            PlayerPrefs.SetString("Prompt", "True");
        }
        else
        {
            image.sprite = PromptOff;
            activePrompt = false;
            PlayerPrefs.SetString("Prompt", "False");
        }
    }

    private void Start()
    {
        image = GetComponent<Image>();
        SetPrompt();
    }
}