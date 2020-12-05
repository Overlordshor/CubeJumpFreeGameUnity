using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public Text CountCoin;

    private int countCoin;
    private int coinReward { get; set; }

    public void Add()
    {
        countCoin++;
        SetCountCoin();
    }

    public void Reward()
    {
        countCoin += coinReward;
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
        coinReward = 100;
    }

    private void SetCountCoin()
    {
        PlayerPrefs.SetInt("Coin", countCoin);
        CountCoin.text = countCoin.ToString();
    }
}