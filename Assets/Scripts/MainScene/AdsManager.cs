using UnityEngine.Advertisements;
using UnityEngine;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private string placementID = "rewardedVideo";
    private Coin coin;

    private GameObject advertisingButton;

    /// <summary>
    /// Called from Unity OnClick () by AdvertisingButton
    /// </summary>
    public void ShowAdvertisements()
    {
        if (Advertisement.IsReady(placementID))
        {
            Advertisement.Show(placementID);
        }
        else
        {
            Debug.LogError("Fail reward video");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log(message);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            switch (gameObject.name)
            {
                case "AdsManagerShop":

                    PlayerPrefs.SetInt(placementId, PlayerPrefs.GetInt(placementId) + 1);
                    coin.Reward();
                    break;

                case "AdsManagerProceed":
                    transform.parent.gameObject.SetActive(false);
                    var game = FindObjectOfType<Game>();
                    game.CreateNewCube();
                    break;
            }

            advertisingButton.SetActive(false);
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    private void Start()
    {
        coin = FindObjectOfType<Coin>();
        if (!PlayerPrefs.HasKey(placementID))
        {
            PlayerPrefs.SetInt(placementID, 0);
        }

        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("3921519", false);
            Advertisement.AddListener(this);
        }
    }

    private void OnEnable()
    {
        advertisingButton = transform.parent.Find("AdvertisingButton").gameObject;
        advertisingButton.SetActive(Advertisement.IsReady(placementID));
    }
}