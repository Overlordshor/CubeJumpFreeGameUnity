using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public Text CountCoin;

    private int countCoin;

    public void Add()
    {
        countCoin++;
        PlayerPrefs.SetInt("Coin", countCoin);
        CountCoin.text = countCoin.ToString();
    }

    public void RefreshCount()
    {
        countCoin = PlayerPrefs.GetInt("Coin");
        CountCoin.text = countCoin.ToString();
    }

    private void Start()
    {
        RefreshCount();
    }
}