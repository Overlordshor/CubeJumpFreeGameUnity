using UnityEngine.Advertisements;
using UnityEngine;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private GameObject _buttonShop, _buttonProceed, _cube, _mainCube;
    private Coin _coin;
    private bool _isShop;
    private readonly bool _adsTestMode = false;

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
        if (Advertisement.IsReady(Keys.ID.PlacementNotReward))
        {
            Advertisement.Show(Keys.ID.PlacementNotReward);
        }
    }

    /// <summary>
    /// Called from Unity OnClick () by AdvertisingButton
    /// </summary>
    public void ShowRewardAdvertisement(bool shop)
    {
        if (Advertisement.IsReady(Keys.ID.PlacementReward))
        {
            _isShop = shop;
            Advertisement.Show(Keys.ID.PlacementReward);
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
            Advertisement.Initialize(Keys.ID.GameAndroid, _adsTestMode);
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
        if (Keys.ID.PlacementReward == placementId)
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
                    var spawn = FindObjectOfType<SpawnCubes>();
                    spawn.GetCube(_cube, _mainCube);
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