using UnityEngine;
using UnityEngine.UI;

public class ButtonAccept : MonoBehaviour
{
    public Sprite Accept, Buy;
    public GameObject Price;

    private Coin coin;
    private Image image;
    private Shop shop;
    private CubeSelection cube;

    private readonly string keyCoin = "Coin";
    private readonly string keyOpen = "Open";

    public void SelectCube()
    {
        if (cube.Open)
        {
            AcceptCube();
        }
        else if (PlayerPrefs.GetInt(keyCoin) >= cube.Cost)
        {
            PlayerPrefs.SetInt(keyCoin, PlayerPrefs.GetInt(keyCoin) - cube.Cost);
            PlayerPrefs.SetString(cube.name, keyOpen);
            cube.Open = true;
            coin.RefreshCount();
            AcceptCube();
        }
        GetButtonImage(cube);
    }

    public void GetButtonImage(CubeSelection cubeSelection)
    {
        cube = cubeSelection;

        if (cube.Open)
        {
            image.sprite = Accept;
        }
        else
        {
            image.sprite = Buy;
        }

        SetActivePrice();
    }

    private void SetActivePrice()
    {
        Price.SetActive(!cube.Open);
    }

    private void AcceptCube()
    {
        shop.SetMaterialCube(cube);
        transform.parent.gameObject.SetActive(false);
    }

    private void Start()
    {
        shop = FindObjectOfType<Shop>();
        coin = FindObjectOfType<Coin>();
        image = GetComponent<Image>();
    }
}