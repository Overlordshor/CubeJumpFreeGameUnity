using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public Text CountCoin;

    private int countCoin;

    public void Add()
    {
        countCoin++;
        SetCountCoin();
    }

    public void Reward()
    {
        countCoin += 200;
        SetCountCoin();
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

    private void SetCountCoin()
    {
        PlayerPrefs.SetInt("Coin", countCoin);
        CountCoin.text = countCoin.ToString();
    }
}