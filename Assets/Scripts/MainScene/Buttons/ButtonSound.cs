using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public Sprite SoundOn, SoundOff;

    private Image image;

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
            PlayerPrefs.SetString("Mute", "False");
            Camera.main.GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            image.sprite = SoundOff;
            PlayerPrefs.SetString("Mute", "True");
            Camera.main.GetComponent<AudioListener>().enabled = false;
        }
    }

    private void Start()
    {
        image = GetComponent<Image>();
        bool mute = PlayerPrefs.GetString("Mute") != "True";
        GetSound(mute);
    }
}