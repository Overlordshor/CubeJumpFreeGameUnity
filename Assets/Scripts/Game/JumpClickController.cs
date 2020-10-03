using UnityEngine;
using UnityEngine.UI;

public class JumpClickController : MonoBehaviour
{
    public GameObject Cube;
    public Text RulesText;
    public GameObject DeactivatedCubes;

    private bool clickDetected;
    private float startTime;
    private Cube gameCube;
    private Game game;

    public void GetControl(GameObject cube)
    {
        gameCube = cube.GetComponentInChildren<Cube>();
    }

    public void GetFinalText()
    {
        Language.PrintAnyLanguage(RulesText, "Click to RESTART", "Нажми, чтобы нажать заново");
        RulesText.gameObject.SetActive(true);
    }

    private void Start()
    {
        gameCube = Cube.GetComponentInChildren<Cube>();
        game = GetComponentInParent<Game>();
        clickDetected = false;
        if (PlayerPrefs.GetString("Prompt") == "True")
        {
            Language.PrintAnyLanguage(RulesText,
                "Press and hold to jump. Get points for every cube hit",
                "Нажми на экран и удерживай, чтобы прыгнуть. Получай очки за каждое попадание по кубу");
            RulesText.gameObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        gameCube.Squeeze(clickDetected);
    }

    private void OnMouseDown()
    {
        clickDetected = true;
        startTime = Time.time;
        if (game?.JumpAttempt == 0 && gameCube.transform.parent == DeactivatedCubes.transform)
        {
            game.Restart();
        }
    }

    private void OnMouseUp()
    {
        clickDetected = false;

        var pushTime = Time.time - startTime;
        gameCube?.Jump(pushTime);

        if (RulesText.gameObject.activeSelf)
        {
            RulesText.gameObject.SetActive(false);
        }
    }
}