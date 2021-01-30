using UnityEngine;
using UnityEngine.UI;

public delegate void OnCompressedCubeDelegate();

public class JumpClickController : MonoBehaviour
{
    private Image _powerJumpFillImage;

    private float _startTime;

    private Cube _gameCube;
    private Game _game;

    public GameObject Cube;
    public Text RulesText;
    public GameObject DeactivatedCubes;

    public Slider PowerJumpBar;
    public GameObject PowerJumpBarFill;

    public event OnCompressedCubeDelegate OnCompressedCube;

    public bool IsClickDetected { get; private set; } = false;

    public void GetControl(GameObject cube)
    {
        _gameCube = cube.GetComponentInChildren<Cube>();
    }

    private void Start()
    {
        _gameCube = Cube.GetComponentInChildren<Cube>();
        _game = GetComponentInParent<Game>();
        _powerJumpFillImage = PowerJumpBarFill.GetComponent<Image>();

        if (PlayerPrefs.GetString("Prompt") == "True")
        {
            Language.PrintAnyLanguage(RulesText,
                "Tap the screen and hold to jump. Get points for every cube hit",
                "Нажми на экран и удерживай, чтобы прыгнуть. Получай очки за каждое попадание по кубу");
            RulesText.gameObject.SetActive(true);
        }
    }

#if UNITY_IOS || UNITY_ANDROID

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                OnCompressedCube();
                IsClickDetected = true;
                _startTime = Time.time;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                IsClickDetected = false;
                var pushTime = Time.time - _startTime;
                _gameCube?.Jump(pushTime);
                GetComponent<AudioSource>().Stop();

                if (RulesText.gameObject.activeSelf)
                {
                    RulesText.gameObject.SetActive(false);
                }

                PowerJumpBar.value = 0f;
            }
        }
    }

#endif

#if UNITY_EDITOR

    public void OnMouseDown()
    {
        OnCompressedCube();
        IsClickDetected = true;
        _startTime = Time.time;
    }

    private void OnMouseUp()
    {
        IsClickDetected = false;
        var pushTime = Time.time - _startTime;
        _gameCube?.Jump(pushTime);
        GetComponent<AudioSource>().Stop();

        if (RulesText.gameObject.activeSelf)
        {
            RulesText.gameObject.SetActive(false);
        }

        PowerJumpBar.value = 0f;
    }

#endif

    private void LateUpdate()
    {
        SetHealthBar();
    }

    private void SetHealthBar()
    {
        if (IsClickDetected && !_game.EndGameButtons.activeSelf)
        {
            var pushTime = Time.time - _startTime;

            PowerJumpBar.value = _gameCube.GetForces(pushTime);

            if (PowerJumpBar.value >= 300f)
            {
                _powerJumpFillImage.color = new Color(1f, 0, 0); // red;
            }
            else if (PowerJumpBar.value > 220f)
            {
                _powerJumpFillImage.color = new Color(1f, 0.5f, 0); // orange;
            }
            else if (PowerJumpBar.value >= 160f)
            {
                _powerJumpFillImage.color = new Color(0, 1f, 0); // green;
            }
            else if (PowerJumpBar.value < 160f)
            {
                _powerJumpFillImage.color = new Color(1f, 1f, 0); // yellow;
            }
        }
    }
}