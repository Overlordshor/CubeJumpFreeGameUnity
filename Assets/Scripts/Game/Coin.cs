using System;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour, ICubeEventComponent
{
    private int _countCoin;
    private Text _textCoin;
    private CollisionHandler _collision;

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

    private void Add()
    {
        _countCoin++;
        SetCountCoin();
    }

    public void AddReward()
    {
        _countCoin += 50;
        SetCountCoin();
    }

    public void RefreshCount()
    {
        _countCoin = PlayerPrefs.GetInt("Coin");
        _textCoin.text = _countCoin.ToString();
    }

    public void Subcribe(CollisionHandler collision)
    {
        _collision = collision;
        SubscribeOnEvents();
    }

    public void Cube_OnCompressedCube()
    {
        throw new NotImplementedException();
    }

    public void Cube_OnHitCube()
    {
        Add();
        _collision.OnHitCube -= Cube_OnHitCube;
    }

    public void Cube_OnJumped()
    {
        throw new NotImplementedException();
    }

    public void Cube_OnFellGround()
    {
        throw new NotImplementedException();
    }

    public void SubscribeOnEvents()
    {
        _collision.OnHitCube += Cube_OnHitCube;
    }
}