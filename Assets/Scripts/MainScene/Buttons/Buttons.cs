using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] private GameObject _shop;
    private float _speed;
    private bool _move = false;
    private RectTransform _buttonRect;
    private Animation _animation;
    private AudioSource _audio;
    private ButtonSound _sound;
    private SceneArrengement _scene;
    private Image _image;

    public void GoAway()
    {
        _speed = 50f;
        _move = true;
        StartCoroutine(nameof(DisableButtons));
    }

    private void Start()
    {
        _buttonRect = GetComponent<RectTransform>();
        _animation = GetComponent<Animation>();
        _audio = GetComponentInParent<AudioSource>();
        _scene = FindObjectOfType<SceneArrengement>();
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        if (_move)
        {
            _buttonRect.offsetMin -= new Vector2(0, _speed);
        }
    }

    private void OnMouseUpAsButton()
    {
        _audio.Play();

        if (!_animation.isPlaying)
        {
            switch (gameObject.name)
            {
                case "SettingButton":
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        transform.GetChild(i).gameObject.SetActive(!transform.GetChild(i).gameObject.activeSelf);
                    }
                    break;

                case "Sound":
                    _sound = gameObject.GetComponent<ButtonSound>();
                    _image.sprite = _sound.GetSprite(_scene.Sound);
                    _sound.GetSound(!_scene.Sound);
                    _scene.Sound = !_scene.Sound;
                    break;

                case "Prompt":
                    var button = gameObject.GetComponent<ButtonPrompt>();
                    _scene.Prompt = !_scene.Prompt;
                    button.GetPrompt();
                    break;

                case "AcceptButton":
                    gameObject.GetComponent<ButtonAccept>().SelectCube();
                    break;

                case "Language":
                    gameObject.GetComponent<ButtonLanguage>().GetLanguage();
                    break;

                case "Restart":
                    gameObject.GetComponent<ButtonsEndGame>().Restart();
                    break;

                case "Return":
                    gameObject.GetComponent<ButtonsEndGame>().ReturnToMenu();
                    break;

                case "AcceptExitButton":
                    gameObject.GetComponent<AcceptExitButton>().Exit();
                    break;

                case "VKButton":
                    Application.OpenURL(Keys.UrlVk);
                    break;

                case "InstaButton":
                    Application.OpenURL(Keys.UrlInstagram);
                    break;

                case "FBButton":
                    Application.OpenURL(Keys.UrlFb);
                    break;

                case "TelegramButton":
                    Application.OpenURL(Keys.UrlTelegram);
                    break;

                case "LeaderboardButton":
                    if (GoogleServicesManager.IsAuthenticate)
                    {
                        GoogleServicesManager.ReportScore(PlayerPrefs.GetInt("Record"));
                        GoogleServicesManager.ShowLeaderboardUI();
                    }
                    else
                    {
                        Debug.LogError("GoogleServices " + GoogleServicesManager.IsAuthenticate);
                    }
                    break;
            }
        }
    }

    private IEnumerator DisableButtons()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    public void PlayClassicGame()
    {
        _scene.StartGame(Mode.Classic);
    }

    public void PlayReductionMode()
    {
        _scene.StartGame(Mode.Reduction);
    }
}