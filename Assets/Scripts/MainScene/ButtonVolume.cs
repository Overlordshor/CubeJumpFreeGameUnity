using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonVolume : MonoBehaviour
{
    public Sprite VolumeOn, VolumeOff;

    private Image image;
    private bool mute = false;

    /// <summary>
    /// For correct entry into the GetVolume() function, you need to invert the mute variable;
    /// </summary>
    private void SetVolume()
    {
        if (PlayerPrefs.GetString("Mute") == "True")
        {
            mute = false;
        }
        else
        {
            mute = true;
        }

        GetVolume();
    }

    public void GetVolume()
    {
        if (mute)
        {
            image.sprite = VolumeOn;
            mute = false;
            PlayerPrefs.SetString("Mute", "False");
            print("Дай звук"); // тут звук есть
        }
        else
        {
            image.sprite = VolumeOff;
            mute = true;
            PlayerPrefs.SetString("Mute", "True");
            print("Убери звук"); // тут звука нет
        }
    }

    private void Start()
    {
        image = GetComponent<Image>();
        SetVolume();
    }
}