using UnityEngine;

public class ButtonsEndGame : MonoBehaviour
{
    private Game game;

    public void Restart()
    {
        PlayerPrefs.SetString(Keys.StartImmediately, "true");
        ReturnToMenu();
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