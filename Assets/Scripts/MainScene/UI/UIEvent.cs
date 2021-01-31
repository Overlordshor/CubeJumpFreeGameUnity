using UnityEngine;

public class UIEvent : MonoBehaviour
{
    private Coin _coin;
    private Score _score;
    private Lives _lives;

    private void Start()
    {
        _coin = GetComponentInChildren<Coin>();
        _score = GetComponentInChildren<Score>();
        _lives = GetComponentInChildren<Lives>();
    }

    public void SubcribeCube(CollisionHandler collision)
    {
        _score.Subcribe(collision);
        _lives.Subcribe(collision);
        _coin.Subcribe(collision);
    }
}