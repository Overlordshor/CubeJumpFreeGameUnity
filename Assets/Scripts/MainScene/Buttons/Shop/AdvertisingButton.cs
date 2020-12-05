using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertisingButton : MonoBehaviour
{
    private string placementID = "rewardedVideo";

    /// <summary>
    /// Called from Unity OnClick () by AdvertisingButton
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Called from Unity OnClick () by ShopButton
    /// </summary>
    public void Show()
    {
        if (PlayerPrefs.GetInt(placementID) < 4)
        {
            gameObject.SetActive(Advertisement.IsReady(placementID));
        }
    }
}