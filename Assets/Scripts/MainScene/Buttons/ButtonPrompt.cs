using UnityEngine;
using UnityEngine.UI;

public class ButtonPrompt : MonoBehaviour
{
    public Sprite PromptOn, PromptOff;
    public bool Prompt { get => prompt; set => prompt = value; }

    private Image image;
    private bool prompt = true;

    private void SetPrompt()
    {
        if (PlayerPrefs.HasKey("Prompt"))
        {
            Prompt = PlayerPrefs.GetString("Prompt") == "True";
        }
        else
        {
            Prompt = true;
        }

        GetPrompt();
    }

    public void GetPrompt()
    {
        if (Prompt)
        {
            image.sprite = PromptOn;
            PlayerPrefs.SetString("Prompt", "True");
        }
        else
        {
            image.sprite = PromptOff;
            PlayerPrefs.SetString("Prompt", "False");
        }
    }

    private void Start()
    {
        image = GetComponent<Image>();
        SetPrompt();
    }
}