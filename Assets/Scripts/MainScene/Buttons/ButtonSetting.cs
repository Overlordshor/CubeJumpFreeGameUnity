using UnityEngine;

public class ButtonSetting : MonoBehaviour
{
    public GameObject SocialGroupBar;

    private void OnEnable()
    {
        ToggleUI(SocialGroupBar);
    }

    private void OnDisable()
    {
        ToggleUI(SocialGroupBar);
    }

    private void ToggleUI(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}