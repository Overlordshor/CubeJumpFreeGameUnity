using UnityEngine.Advertisements;
using UnityEngine;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private string placementID = "rewardedVideo";
    private Coin coin;

    public void ShowAdvertisements()
    {
        if (Advertisement.IsReady(placementID))
        {
            Advertisement.Show(placementID);
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
            coin.Reward();
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

        Advertisement.AddListener(this);

        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("3921519", false);
        }
    }
}