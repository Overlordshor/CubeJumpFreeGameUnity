using UnityEngine;

public class UIEvent : MonoBehaviour, ICubeEventComponent
{
    private CollisionHandler _collision;
    private Coin _coin;
    private Score _score;
    private Lives _lives;

    private void Start()
    {
        _coin = GetComponentInChildren<Coin>();
        _score = GetComponentInChildren<Score>();
        _lives = GetComponentInChildren<Lives>();
    }

    private void AddReward()
    {
        _score.Add();
        _coin.Add();
    }

    public void Cube_OnCompressedCube()
    {
        throw new System.NotImplementedException();
    }

    public void Cube_OnFellGround()
    {
        //_score.PrintBuffOnScreen();
        _collision.OnFellGround -= Cube_OnFellGround;
        _collision.OnHitCube -= Cube_OnHitCube;
        _collision.OnJumped -= Cube_OnJumped;
    }

    public void Cube_OnHitCube()
    {
        AddReward();
        _lives.Add();
        //_score.PrintBuffOnScreen();
        _collision.OnHitCube -= Cube_OnHitCube;
    }

    public void Cube_OnJumped()
    {
        _lives.Remove();
        _collision.OnJumped -= Cube_OnJumped;
    }

    public void SubscribeOnEvent()
    {
        _collision.OnFellGround += Cube_OnFellGround;
        _collision.OnHitCube += Cube_OnHitCube;
        _collision.OnJumped += Cube_OnJumped;
    }

    public void IntroduceCube(CollisionHandler collision)
    {
        _collision = collision;
        SubscribeOnEvent();
    }
}