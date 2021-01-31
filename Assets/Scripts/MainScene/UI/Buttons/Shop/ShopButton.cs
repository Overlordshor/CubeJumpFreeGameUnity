using UnityEngine;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private GameObject _playClassicButton, _playReductionButton, _socialGroupBar;

    private void OnEnable()
    {
        ToggleAllUI();
        transform.Find("AdvertisingButton").GetComponent<AdvertisingButton>().Display(AdvertisingType.CoinReward);
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
        ToggleUI(_playReductionButton);
        ToggleUI(_socialGroupBar);
    }
}