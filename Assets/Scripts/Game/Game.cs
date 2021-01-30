using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class Game : MonoBehaviour, ICubeEventComponent
{
    private int _jumpAttempt = 1;

    private Score _score;

    private Coin _coin;
    private AdsManager _adsManager;
    private CollisionHandler _collision;

    public GameObject DeactivatedCubes;
    public GameObject EndGameButtons, ExitPanel, Buttons;

    public Text LivesText;

    public Transform DeathStars;

    public Mode IsMode { get; set; }

    private void Start()
    {
        _score = GetComponent<Score>();
        _coin = GetComponent<Coin>();

        _adsManager = FindObjectOfType<AdsManager>();
        _adsManager.InitializeAdvertisements();
    }

    private void Update()
    {
        ShowExitPanel();
        Language.PrintAnyLanguage(LivesText, "Lives: " + _jumpAttempt.ToString(), "Жизней: " + _jumpAttempt.ToString());
    }

    private void DisplayEndGameButtons()
    {
        EndGameButtons.SetActive(true);
        EndGameButtons.transform.Find("AdvertisingButton").GetComponent<AdvertisingButton>().Display(false);
    }

    public void Restart()
    {
        PlayerPrefs.SetInt(Keys.CountGames, PlayerPrefs.GetInt(Keys.CountGames) + 1);

        var countGames = PlayerPrefs.GetInt(Keys.CountGames);
        if (countGames % 5 == 0)
        {
            _adsManager.ShowNotRewardAdvertisement();

            PlayerPrefs.SetInt(Keys.CountRewardAdvertising, 0);
        }
        if (countGames == 13)
        {
            GoogleServicesManager.UnlockAchievement(Keys.AchievementBakersDozen);
            PlayerPrefs.SetInt(Keys.CountGames, 0);
        }

        PlayerPrefs.Save();

        PlayerPrefs.DeleteKey(Keys.ContinuedAdvertising);

        PlayerPrefs.SetInt(Keys.Mode, (int)IsMode);
        SceneManager.LoadScene(0); // Main;
    }

    private void GetReward()
    {
        _score.Add();
        _coin.Add();
    }

    private void ShowExitPanel()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitPanel.SetActive(true);
                Buttons?.SetActive(false);
            }
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitPanel.SetActive(true);
            Buttons?.SetActive(false);
        }
#endif
    }

    public void Cube_OnCompressedCube()
    {
        throw new System.NotImplementedException();
    }

    public void Cube_OnHitCube()
    {
        GetReward();
        _collision.OnFellGround -= Cube_OnHitCube;
    }

    public void Cube_OnJumped()
    {
        if (_jumpAttempt >= 1)
        {
            LivesText.gameObject.SetActive(true);
        }

        _jumpAttempt--;
        _collision.OnFellGround -= Cube_OnJumped;
    }

    public void Cube_OnFellGround()
    {
        if (/*_cube.IsJumped && !_cube.IsPlayerControl && */_jumpAttempt == 0)
        {
            DisplayEndGameButtons();
            _collision.OnFellGround -= Cube_OnFellGround;
        }
    }

    public void SubscribeOnEvent()
    {
        _collision.OnFellGround += Cube_OnFellGround;
        _collision.OnFellGround += Cube_OnJumped;
        _collision.OnFellGround += Cube_OnHitCube;
    }

    public void IntroduceCube(GameObject cube)
    {
        _collision = cube.GetComponent<CollisionHandler>();
        SubscribeOnEvent();
    }
}