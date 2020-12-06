using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public GameObject PlayButton, SocialGroupBar;

    private void OnEnable()
    {
        ToggleAllUI();
        transform.Find("AdvertisingButton").GetComponent<AdvertisingButton>().Display();
    }

    private void OnDisable()
    {
        ToggleAllUI();
    }

    private void ToggleUI(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    private void ToggleAllUI()
    {
        ToggleUI(PlayButton);
        ToggleUI(SocialGroupBar);
    }
}