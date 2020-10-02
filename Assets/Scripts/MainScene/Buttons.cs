using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public Sprite VolumeOn, VolumeOff;

    private bool mute;

    private Vector3 originalScale;
    private Vector3 pressedScale;
    private float speed;
    private bool move;
    private RectTransform button;

    public void GoAway()
    {
        speed = 5f;
        move = true;
        Destroy(gameObject, 1f);
    }

    private void Start()
    {
        originalScale = new Vector3(0.01915709f, 0.01915709f);
        pressedScale = new Vector3(0.027f, 0.027f);
        button = GetComponent<RectTransform>();
        move = false;
        SetVolume();
    }

    private void SetVolume()
    {
        if (PlayerPrefs.GetString("Mute") == "True")
        {
            mute = true;
        }
        else
        {
            mute = false;
        }
    }

    private void Update()
    {
        if (move)
        {
            button.offsetMin -= new Vector2(0, speed);
        }
    }

    //private void OnMouseDown()
    //{
    //    SetLocalScale(pressedScale);
    //}

    //private void OnMouseUp()
    //{
    //    SetLocalScale(originalScale);
    //}

    protected void OnMouseUpAsButton()
    {
        switch (gameObject.name)
        {
            case "SettingButton":
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(!transform.GetChild(i).gameObject.activeSelf);
                }
                break;

            case "Volume":
                if (mute)
                {
                    GetComponent<Image>().sprite = VolumeOn;
                    mute = false;
                    PlayerPrefs.SetString("Mute", "False");
                    print("Дай звук"); // тут звук есть
                }
                else
                {
                    GetComponent<Image>().sprite = VolumeOff;
                    mute = true;
                    PlayerPrefs.SetString("Mute", "True");
                    print("Убери звук"); // тут звука нет
                }
                break;
        }
    }

    private void SetLocalScale(Vector3 scale)
    {
        transform.localScale = scale;
    }
}