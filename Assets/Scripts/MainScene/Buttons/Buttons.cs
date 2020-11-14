using UnityEngine;

public class Buttons : MonoBehaviour
{
    private float speed;
    private bool move = false;
    private RectTransform buttonRect;
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
        buttonRect = GetComponent<RectTransform>();
        animation = GetComponent<Animation>();
        audio = GetComponentInParent<AudioSource>();
    }

    private void Update()
    {
        if (move)
        {
            buttonRect.offsetMin -= new Vector2(0, speed);
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
                    gameObject.GetComponent<ButtonVolume>().GetVolume();
                    break;

                case "Prompt":
                    gameObject.GetComponent<ButtonPrompt>().GetPrompt();
                    break;

                case "ShopButton":
                    gameObject.transform.GetChild(0).gameObject.SetActive(!gameObject.transform.GetChild(0).gameObject.activeSelf);
                    break;

                case "CancelButton":
                    transform.parent.gameObject.SetActive(false);
                    break;

                case "AcceptButton":
                    gameObject.GetComponent<ButtonAccept>().SelectCube();
                    break;

                case "Language":
                    gameObject.GetComponent<ButtonLanguage>().GetLanguage();
                    break;

                case "AdvertisingButton":
                    print("SHOW ADVERTISING");
                    break;

                case "Restart":
                    gameObject.GetComponent<ButtonsEndGame>().Restart();
                    break;

                case "Return":
                    gameObject.GetComponent<ButtonsEndGame>().ReturnToMenu();
                    break;
            }
        }
    }
}