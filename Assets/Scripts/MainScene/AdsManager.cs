using UnityEngine.Advertisements;
using UnityEngine;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    public GameObject ButtonShop, ButtonProceed;

    private Coin coin;
    private bool shop;

    public void ShowNotRewardAdvertisement()
    {
        if (Advertisement.IsReady(Keys.PlacementNotRewardId))
        {
            Advertisement.Show(Keys.PlacementNotRewardId);
        }
    }

    /// <summary>
    /// Called from Unity OnClick () by AdvertisingButton
    /// </summary>
    public void ShowRewardAdvertisement(bool shop)
    {
        if (Advertisement.IsReady(Keys.PlacementRewardId))
        {
            this.shop = shop;
            Advertisement.Show(Keys.PlacementRewardId);
        }
        else
        {
            Debug.LogError("Fail reward video");
        }
    }

    public void InitializeAdvertisements()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(Keys.GameAndroidID, Keys.AdsTestMode);
            Advertisement.AddListener(this);

            if (!PlayerPrefs.HasKey(Keys.CountGames))
            {
                PlayerPrefs.SetInt(Keys.CountGames, 0);
            }
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log(message);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (Keys.PlacementRewardId == placementId)
        {
            if (showResult == ShowResult.Finished)
            {
                if (shop)
                {
                    coin.Reward();
                    PlayerPrefs.SetInt(Keys.CountRewardAdvertising, PlayerPrefs.GetInt(Keys.CountRewardAdvertising) + 1);
                }
                else
                {
                    var game = FindObjectOfType<Game>();
                    game.CreateNewCube();
                    PlayerPrefs.SetString(Keys.ContinuedAdvertising, "True");
                }
            }
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("INITIALIZED " + placementId);
    }

    private void Start()
    {
        InitializeAdvertisements();

        coin = FindObjectOfType<Coin>();

        if (!PlayerPrefs.HasKey(Keys.CountRewardAdvertising))
        {
            PlayerPrefs.SetInt(Keys.CountRewardAdvertising, 0);
        }
    }

    private void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}