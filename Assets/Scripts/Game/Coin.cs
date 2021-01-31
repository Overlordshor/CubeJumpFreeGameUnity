using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    private int _countCoin;
    private Text _textCoin;

    private void Start()
    {
        _textCoin = GetComponentInChildren<Text>();
        RefreshCount();
    }

    private void SetCountCoin()
    {
        PlayerPrefs.SetInt("Coin", _countCoin);
        _textCoin.text = _countCoin.ToString();
    }

    public void Add()
    {
        _countCoin++;
        SetCountCoin();
    }

    public void Reward()
    {
        _countCoin += 50;
        SetCountCoin();
    }

    public void RefreshCount()
    {
        _countCoin = PlayerPrefs.GetInt("Coin");
        _textCoin.text = _countCoin.ToString();
    }
}