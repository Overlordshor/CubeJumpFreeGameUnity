using UnityEngine;
using UnityEngine.Advertisements;

public enum AdvertisingType
{
    CoinReward,
    ContinueReward
}

public class AdvertisingButton : MonoBehaviour
{
    /// <summary>
    /// Show button to show advertisements in store (true), after death (false)
    /// </summary>
    /// <param name="shop"></param>
    public void Display(AdvertisingType type)
    {
        switch (type)
        {
            case AdvertisingType.CoinReward:
                if (PlayerPrefs.GetInt(Keys.CountRewardAdvertising) < 4)
                {
                    gameObject.SetActive(Advertisement.IsReady(Keys.ID.PlacementReward));
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;

            case AdvertisingType.ContinueReward:
                if (PlayerPrefs.HasKey(Keys.ContinuedAdvertising))
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(Advertisement.IsReady(Keys.ID.PlacementReward));
                }
                break;
        }
    }
}