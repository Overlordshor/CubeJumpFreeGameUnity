using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public Sprite SoundOn, SoundOff;

    private Image image;
    private SceneArrengement scene;

    private string trueResult = "True", falseResult = "False";

    public Sprite GetSprite(bool sound)
    {
        if (sound)
        {
            return SoundOn;
        }
        else
        {
            return SoundOff;
        }
    }

    public void GetSound(bool mute)
    {
        if (mute)
        {
            image.sprite = SoundOn;
            PlayerPrefs.SetString(Keys.Mute, falseResult);
            AudioListener.volume = 1f;
        }
        else
        {
            image.sprite = SoundOff;
            PlayerPrefs.SetString(Keys.Mute, trueResult);
            AudioListener.volume = 0f;
        }
    }

    private void Start()
    {
        scene = FindObjectOfType<SceneArrengement>();
        image = GetComponent<Image>();
        bool mute = PlayerPrefs.GetString(Keys.Mute) != trueResult;
        GetSound(mute);
    }
}