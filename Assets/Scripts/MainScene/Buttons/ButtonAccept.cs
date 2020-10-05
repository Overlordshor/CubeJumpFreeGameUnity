using UnityEngine;
using UnityEngine.UI;

public class ButtonAccept : MonoBehaviour
{
    public Sprite Accept, Buy;

    private ShopScroller shopScroller;
    private Coin coin;
    private Image image;
    private bool openCube;

    private int costCube = 200;
    private string keyCoin = "Coin";
    private string keyOpen = "Open";

    public void SelectCube()
    {
        if (openCube)
        {
        }
        else if (PlayerPrefs.GetInt(keyCoin) >= costCube)
        {
            PlayerPrefs.SetInt(keyCoin, PlayerPrefs.GetInt(keyCoin) - costCube);
            PlayerPrefs.SetString(shopScroller.GetNameCube(), keyOpen);
            coin.RefreshCount();
        }
        GetImage();
    }

    public void GetImage()
    {
        SetImage();
        if (openCube)
        {
            image.sprite = Accept;
        }
        else
        {
            image.sprite = Buy;
        }
    }

    private void SetImage()
    {
        if (PlayerPrefs.GetString(shopScroller.GetNameCube()) == keyOpen)
        {
            openCube = true;
        }
        else
        {
            openCube = false;
        }
    }

    private void Start()
    {
        shopScroller = FindObjectOfType<ShopScroller>();
        coin = FindObjectOfType<Coin>();
        image = GetComponent<Image>();
        GetImage();
    }
}