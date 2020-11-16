using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject ClickDetector, PlayButton, SocialGroupBar;

    private void OnEnable()
    {
        ClickDetector.SetActive(false);
        PlayButton.SetActive(false);
    }

    private void OnDisable()
    {
        ClickDetector?.SetActive(true);
        PlayButton.SetActive(true);
    }
}