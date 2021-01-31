using UnityEngine;
using UnityEngine.UI;

public delegate void OnCompressedCubeDelegate();

public class JumpClickController : MonoBehaviour
{
    private Image _powerJumpFillImage;

    private float _startTime;

    private Cube _gameCube;
    private Lives _lives;

    [SerializeField] private Text _classicRulesText, _reductionRulesText;
    [SerializeField] private GameObject _rules;

    public GameObject Cube;

    public Slider PowerJumpBar;
    public GameObject PowerJumpBarFill;

    public event OnCompressedCubeDelegate OnCompressedCube;

    public bool IsClickDetected { get; private set; } = false;

    private void Start()
    {
        _gameCube = Cube.GetComponentInChildren<Cube>();
        _lives = FindObjectOfType<Lives>();
        _powerJumpFillImage = PowerJumpBarFill.GetComponent<Image>();

        if (PlayerPrefs.GetString("Prompt") == "True")
        {
            _rules.SetActive(true);

            switch (Game.IsMode)
            {
                case Mode.Classic:  
                    _classicRulesText.gameObject.SetActive(true);
                    break;

                case Mode.Reduction:
                    _reductionRulesText.gameObject.SetActive(true);
                    break;
            }
        }
    }

    private void Update()
    {
#if UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (OnCompressedCube != null)
                {
                    OnCompressedCube();
                }
                IsClickDetected = true;
                _startTime = Time.time;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                IsClickDetected = false;
                var pushTime = Time.time - _startTime;
                _gameCube?.Jump(pushTime);
                GetComponent<AudioSource>().Stop();

                _rules.SetActive(false);

                PowerJumpBar.value = 0f;
            }
        }
#endif
        SetHealthBar();
    }

#if UNITY_EDITOR

    private void OnMouseDown()
    {
        if (OnCompressedCube != null)
        {
            OnCompressedCube();
        }
        IsClickDetected = true;
        _startTime = Time.time;
    }

    private void OnMouseUp()
    {
        IsClickDetected = false;
        var pushTime = Time.time - _startTime;
        _gameCube?.Jump(pushTime);
        GetComponent<AudioSource>().Stop();

        _rules.SetActive(false);

        PowerJumpBar.value = 0f;
    }

#endif

    private void SetHealthBar()
    {
        if (IsClickDetected && !_lives.Ended())
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

    public void GetControl(GameObject cube)
    {
        _gameCube = cube.GetComponent<Cube>();
    }
}