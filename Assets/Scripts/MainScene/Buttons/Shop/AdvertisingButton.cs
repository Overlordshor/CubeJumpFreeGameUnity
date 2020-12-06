using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertisingButton : MonoBehaviour
{
    private string placementID = "rewardedVideo";

    public void Display()
    {
        if (PlayerPrefs.GetInt(placementID) < 4)
        {
            gameObject.SetActive(Advertisement.IsReady(placementID));
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnMouseUpAsButton()
    {
        Display();
    }
}