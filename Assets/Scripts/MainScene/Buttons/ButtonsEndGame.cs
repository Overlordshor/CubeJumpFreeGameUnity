using System.Collections;
using UnityEngine;

public class ButtonsEndGame : MonoBehaviour
{
    private Game game;
    private string keyRestart = "Restart";

    public void Restart()
    {
        StartCoroutine("RestartGame");
    }

    public void ReturnToMenu()
    {
        game.Restart();
    }

    private void Start()
    {
        game = FindObjectOfType<Game>();
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2f);
        PlayerPrefs.SetString(keyRestart, "true");
        ReturnToMenu();
    }
}