using UnityEngine;
using UnityEngine.UI;

public class JumpClickController : MonoBehaviour
{
    public GameObject Cube;
    public Text RulesText;
    public GameObject DeactivatedCubes;

    public Slider PowerJumpBar;
    public GameObject PowerJumpBarFill;

    private Image powerJumpFillImage;

    private bool clickDetected;
    private float startTime;
    private Cube gameCube;
    private Game game;

    public void GetControl(GameObject cube)
    {
        gameCube = cube.GetComponentInChildren<Cube>();
    }

    private void Start()
    {
        gameCube = Cube.GetComponentInChildren<Cube>();
        game = GetComponentInParent<Game>();
        powerJumpFillImage = PowerJumpBarFill.GetComponent<Image>();
        clickDetected = false;

        if (PlayerPrefs.GetString("Prompt") == "True")
        {
            Language.PrintAnyLanguage(RulesText,
                "Tap the screen and hold to jump. Get points for every cube hit",
                "Нажми на экран и удерживай, чтобы прыгнуть. Получай очки за каждое попадание по кубу");
            RulesText.gameObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        gameCube.Squeeze(clickDetected);
    }

#if UNITY_IOS || UNITY_ANDROID

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                ClickToScreen();
            }
            if (touch.phase == TouchPhase.Ended)
            {
                EndClick();
            }
        }
    }

#endif

    private void LateUpdate()
    {
        SetHealthBar();
    }

#if UNITY_EDITOR

    private void OnMouseDown()
    {
        ClickToScreen();
    }

    private void OnMouseUp()
    {
        EndClick();
    }

#endif

    private void ClickToScreen()
    {
        clickDetected = true;
        gameCube?.PlayAudioSqueeze(clickDetected);

        startTime = Time.time;
    }

    private void EndClick()
    {
        clickDetected = false;
        var pushTime = Time.time - startTime;
        gameCube?.Jump(pushTime);
        GetComponent<AudioSource>().Stop();

        if (RulesText.gameObject.activeSelf)
        {
            RulesText.gameObject.SetActive(false);
        }

        PowerJumpBar.value = 0f;
    }

    private void SetHealthBar()
    {
        if (clickDetected && !game.EndGameButtons.activeSelf)
        {
            var pushTime = Time.time - startTime;

            PowerJumpBar.value = gameCube.GetForces(pushTime);

            if (PowerJumpBar.value >= 300f)
            {
                powerJumpFillImage.color = new Color(1f, 0, 0); // red;
            }
            else if (PowerJumpBar.value > 220f)
            {
                powerJumpFillImage.color = new Color(1f, 0.5f, 0); // orange;
            }
            else if (PowerJumpBar.value >= 160f)
            {
                powerJumpFillImage.color = new Color(0, 1f, 0); // green;
            }
            else if (PowerJumpBar.value < 160f)
            {
                powerJumpFillImage.color = new Color(1f, 1f, 0); // yellow;
            }
        }
    }
}