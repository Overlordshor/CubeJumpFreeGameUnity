using UnityEngine;

public class ButtonsEndGame : MonoBehaviour
{
    private Game game;
    private string keyRestart = "Restart";

    public void Restart()
    {
        PlayerPrefs.SetString(keyRestart, "true");
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