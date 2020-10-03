using UnityEngine;
using UnityEngine.UI;

public class ButtonVolume : MonoBehaviour
{
    public Sprite VolumeOn, VolumeOff;

    private Image image;
    private bool mute = false;

    public void GetVolume()
    {
        if (mute)
        {
            image.sprite = VolumeOn;
            mute = false;
            PlayerPrefs.SetString("Mute", "False");
            Camera.main.GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            image.sprite = VolumeOff;
            mute = true;
            PlayerPrefs.SetString("Mute", "True");
            Camera.main.GetComponent<AudioListener>().enabled = false;
        }
    }

    /// <summary>
    /// For correct entry into the GetVolume() function, you need to invert the mute variable;
    /// </summary>
    private void SetVolume()
    {
        mute = PlayerPrefs.GetString("Mute") != "True";

        GetVolume();
    }

    private void Start()
    {
        image = GetComponent<Image>();
        SetVolume();
    }
}