using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class Game : MonoBehaviour
{
    public GameObject DeactivatedCubes;
    public GameObject EndGameButtons, ExitPanel, Buttons;

    public Text LivesText;

    private SpawnCubes _cubeSpawner;
    private Score _score;
    private Coin _coin;
    private AudioSource _audioBrokenBox;
    private AdsManager _adsManager;

    public int JumpAttempt { get; set; } = 1;

    public bool AppearedNewCube { get; set; } = false;

    public Mode IsMode { get; set; }

    public void DisplayButtons()
    {
        if (JumpAttempt == 0)
        {
            EndGameButtons.SetActive(true);
            EndGameButtons.transform.Find("AdvertisingButton").GetComponent<AdvertisingButton>().Display(false);
        }
    }

    public void CreateNewCube()
    {
        if (!AppearedNewCube)
        {
            _cubeSpawner.GetNewCube();
            JumpAttempt++;
            AppearedNewCube = true;
            if (EndGameButtons.activeSelf)
            {
                EndGameButtons.SetActive(false);
            }
        }
    }

    public void LoseJumpAttempt()
    {
        if (JumpAttempt >= 1)
        {
            LivesText.gameObject.SetActive(true);
            JumpAttempt--;
        }
    }

    public void Restart()
    {
        PlayerPrefs.SetInt(Keys.CountGames, PlayerPrefs.GetInt(Keys.CountGames) + 1);

        var countGames = PlayerPrefs.GetInt(Keys.CountGames);
        if (countGames % 5 == 0)
        {
            _adsManager.ShowNotRewardAdvertisement();

            PlayerPrefs.SetInt(Keys.CountRewardAdvertising, 0);
            if (countGames == 13)
            {
                GoogleServicesManager.UnlockAchievement(Keys.AchievementBakersDozen);
            }
            if (countGames == 15)
            {
                PlayerPrefs.SetInt(Keys.CountGames, 0);
            }
        }

        PlayerPrefs.Save();

        PlayerPrefs.DeleteKey(Keys.ContinuedAdvertising);

        PlayerPrefs.SetInt(Keys.Mode, (int)IsMode);
        SceneManager.LoadScene(0); // Main;
    }

    public void PlayAudioBrokenBox()
    {
        _audioBrokenBox.Play();
    }

    public void GetReward()
    {
        if (!AppearedNewCube)
        {
            _score.Add();
            _coin.Add();
        }
    }

    private void Start()
    {
        _cubeSpawner = GetComponent<SpawnCubes>();
        _score = GetComponent<Score>();
        _coin = GetComponent<Coin>();
        _audioBrokenBox = GetComponent<AudioSource>();

        _adsManager = FindObjectOfType<AdsManager>();
        _adsManager.InitializeAdvertisements();
    }

    private void Update()
    {
        ShowExitPanel();
        Language.PrintAnyLanguage(LivesText, "Lives: " + JumpAttempt.ToString(), "Жизней: " + JumpAttempt.ToString());
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
}