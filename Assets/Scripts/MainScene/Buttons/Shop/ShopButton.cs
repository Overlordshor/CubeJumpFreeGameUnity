using UnityEngine;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private GameObject _playClassicButton, PlayReductionButton, _socialGroupBar;

    private void OnEnable()
    {
        ToggleAllUI();
        transform.Find("AdvertisingButton").GetComponent<AdvertisingButton>().Display(true);
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
        ToggleUI(_playClassicButton);
        ToggleUI(PlayReductionButton);
        ToggleUI(_socialGroupBar);
    }
}