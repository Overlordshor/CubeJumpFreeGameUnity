using UnityEngine;
using UnityEngine.UI;

public class ButtonAccept : MonoBehaviour
{
    public Sprite Accept, Buy;
    public GameObject Price;

    private Image image;
    private Coin coin;
    private Shop shop;

    private readonly string keyCoin = "Coin";
    private readonly string keyOpen = "Open";

    public void SelectCube()
    {
        var selectCube = shop.GetSelectCube();
        if (selectCube.Open)
        {
            AcceptCube(selectCube);
        }
        else if (PlayerPrefs.GetInt(keyCoin) >= selectCube.Cost)
        {
            PlayerPrefs.SetInt(keyCoin, PlayerPrefs.GetInt(keyCoin) - selectCube.Cost);
            selectCube.Open = true;
            PlayerPrefs.SetString(selectCube.ID, keyOpen);
            coin.RefreshCount();
            AcceptCube(selectCube);

            GoogleServicesManager.UnlockAchievement(Keys.AchievementBestBuyer);
        }
        GetButtonImage(selectCube);
    }

    public void GetButtonImage(CubeData cube)
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }

        image.sprite = cube.Open ? Accept : Buy;

        Price.SetActive(!cube.Open);
    }

    private void AcceptCube(CubeData selectCube)
    {
        shop.SetMaterialCube(selectCube);
        transform.parent.gameObject.SetActive(false);
    }

    private void Start()
    {
        shop = FindObjectOfType<Shop>();
        coin = FindObjectOfType<Coin>();
        image = GetComponent<Image>();
    }
}