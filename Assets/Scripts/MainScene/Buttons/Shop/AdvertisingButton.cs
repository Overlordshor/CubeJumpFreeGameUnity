using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertisingButton : MonoBehaviour
{
    public void Display(bool shop)
    {
        if (shop)
        {
            if (PlayerPrefs.GetInt(Keys.CountRewardAdvertising) < 4)
            {
                gameObject.SetActive(Advertisement.IsReady(Keys.PlacementRewardId));
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (PlayerPrefs.HasKey(Keys.ContinuedAdvertising))
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(Advertisement.IsReady(Keys.PlacementRewardId));
            }
        }
    }
}