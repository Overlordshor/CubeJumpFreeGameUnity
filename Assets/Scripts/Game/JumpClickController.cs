using UnityEngine;
using UnityEngine.UI;

public class JumpClickController : MonoBehaviour
{
    public GameObject Cube;
    public Text RulesText;

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
        RulesText.text = "Click to RESTART";
        RulesText.gameObject.SetActive(true);
    }

    private void Start()
    {
        gameCube = Cube.GetComponentInChildren<Cube>();
        game = GetComponentInParent<Game>();
        clickDetected = false;
        RulesText.text = "Press and hold to jump. Get points for every cube hit";
        RulesText.gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        gameCube.Squeeze(clickDetected);
    }

    private void OnMouseDown()
    {
        clickDetected = true;
        startTime = Time.time;
        if (game.JumpAttempt == 0)
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