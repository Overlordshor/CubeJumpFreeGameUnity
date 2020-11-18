using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject PlayButton, SocialGroupBar;

    private void OnEnable()
    {
        ToggleAllUI();
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