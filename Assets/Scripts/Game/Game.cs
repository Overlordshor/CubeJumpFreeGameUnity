using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject DeactivatedCubes;
    public GameObject EndGameButtons, ExitPanel, Buttons;

    public Text LivesText;

    private SpawnCubes cubeSpawner;
    private Score score;
    private Coin coin;
    private AudioSource audioBrokenBox;
    private AdsManager adsManager;

    public int JumpAttempt { get; set; } = 1;

    public bool AppearedNewCube { get; set; } = false;

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
            cubeSpawner.GetNewCube();
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
            adsManager.ShowNotRewardAdvertisement();

            PlayerPrefs.SetInt(Keys.PlacementRewardId, 0);
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

        Scene activeScene;
        if (PlayerPrefs.GetString(Keys.StartImmediately) == "true")
        {
            activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.buildIndex);
        }
        else
        {
            SceneManager.LoadScene(0); // Main;
        }
    }

    public void PlayAudioBrokenBox()
    {
        audioBrokenBox.Play();
    }

    public void GetReward()
    {
        if (!AppearedNewCube)
        {
            score.Add();
            coin.Add();
        }
    }

    private void Start()
    {
        cubeSpawner = GetComponent<SpawnCubes>();
        score = GetComponent<Score>();
        coin = GetComponent<Coin>();
        audioBrokenBox = GetComponent<AudioSource>();

        adsManager = FindObjectOfType<AdsManager>();
        adsManager.InitializeAdvertisements();
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