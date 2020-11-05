using UnityEngine;

public class ButtonsEndGame : MonoBehaviour
{
    public GameObject ClickDetector;

    private Game game;

    public void Restart() // не работает
    {
        ReturnToMenu();
        ClickDetector.GetComponent<GameArrengement>().StartGame();
    }

    public void ReturnToMenu()
    {
        game.Restart();
    }

    private void Start()
    {
        game = FindObjectOfType<Game>();
    }
}