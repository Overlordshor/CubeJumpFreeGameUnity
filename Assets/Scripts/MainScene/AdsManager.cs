using UnityEngine.Advertisements;
using UnityEngine;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private GameObject _buttonShop, _buttonProceed;
    private Coin _coin;
    private bool _isShop;

    private void Start()
    {
        InitializeAdvertisements();

        _coin = FindObjectOfType<Coin>();

        if (!PlayerPrefs.HasKey(Keys.CountRewardAdvertising))
        {
            PlayerPrefs.SetInt(Keys.CountRewardAdvertising, 0);
        }
    }

    private void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }

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
            _isShop = shop;
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
                if (_isShop)
                {
                    _coin.Reward();
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
}