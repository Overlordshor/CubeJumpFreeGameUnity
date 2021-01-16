using UnityEngine;

public class ButtonsEndGame : MonoBehaviour
{
    private Game _game;

    public void Restart()
    {
        PlayerPrefs.SetString(Keys.StartImmediately, "true");
        ReturnToMenu();
    }

    public void ReturnToMenu()
    {
        _game.Restart();
    }

    private void Start()
    {
        _game = FindObjectOfType<Game>();
    }
}