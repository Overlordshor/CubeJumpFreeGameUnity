using UnityEngine;

public class Buttons : MonoBehaviour
{
    public GameObject ButtonVolume, ButtonPrompt, Shop, Language, Acept, AdvertisingButton;

    private float speed;
    private bool move = false;
    private RectTransform button;
    private new Animation animation;
    private new AudioSource audio;

    public void GoAway()
    {
        speed = 50f;
        move = true;
        Destroy(gameObject, 2f);
    }

    private void Start()
    {
        button = GetComponent<RectTransform>();
        animation = GetComponent<Animation>();
        audio = GetComponentInParent<AudioSource>();
    }

    private void Update()
    {
        if (move)
        {
            button.offsetMin -= new Vector2(0, speed);
        }
    }

    private void OnMouseUpAsButton()
    {
        audio.Play();

        if (!animation.isPlaying)
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
                    ButtonVolume.GetComponent<ButtonVolume>().GetVolume();
                    break;

                case "Prompt":
                    ButtonPrompt.GetComponent<ButtonPrompt>().GetPrompt();
                    break;

                case "ShopButton":
                    Shop.SetActive(!Shop.activeSelf);
                    break;

                case "CancelButton":
                    Shop.SetActive(false);
                    break;

                case "AcceptButton":
                    Acept.GetComponent<ButtonAccept>().SelectCube();
                    break;

                case "Language":
                    Language.GetComponent<ButtonLanguage>().GetLanguage();
                    break;

                case "AdvertisingButton":
                    print("SHOW ADVERTISING");
                    break;
            }
        }
    }
}