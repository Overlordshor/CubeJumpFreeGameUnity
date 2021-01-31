using UnityEngine;
using UnityEngine.UI;

public class ButtonPrompt : MonoBehaviour
{
    public Sprite PromptOn, PromptOff;

    private Image image;

    private SceneArrengement scene;

    public void GetPrompt()
    {
        if (scene.Prompt)
        {
            image.sprite = PromptOn;
            PlayerPrefs.SetString(Keys.Prompt, "True");
        }
        else
        {
            image.sprite = PromptOff;
            PlayerPrefs.SetString(Keys.Prompt, "False");
        }
    }

    private void Start()
    {
        image = GetComponent<Image>();
        scene = FindObjectOfType<SceneArrengement>();
        scene.SetPrompt();
        GetPrompt();
    }
}