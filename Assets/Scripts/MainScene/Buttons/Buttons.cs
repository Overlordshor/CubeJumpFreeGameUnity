using UnityEngine;

public class Buttons : MonoBehaviour
{
    public GameObject ButtonVolume, ButtonPrompt, Shop, Language, Acept;

    private float speed;
    private bool move = false;
    private RectTransform button;

    public void GoAway()
    {
        speed = 5f;
        move = true;
        Destroy(gameObject, 1f);
    }

    private void Start()
    {
        button = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (move)
        {
            button.offsetMin -= new Vector2(0, speed);
        }
    }

    protected void OnMouseUpAsButton()
    {
        GetComponentInParent<AudioSource>().Play();
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

            case "Cancel":
                Shop.SetActive(false);
                break;

            case "Accept":
                Acept.GetComponent<ButtonAccept>().SelectCube();
                break;

            case "Language":
                Language.GetComponent<ButtonLanguage>().GetLanguage();
                break;
        }
    }
}