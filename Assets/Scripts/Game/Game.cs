using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class Game : MonoBehaviour, ICubeEventComponent
{
    private int _lives = 1;

    private AdsManager _adsManager;
    private CollisionHandler _collision;

    [SerializeField] private GameObject _canvas;

    public GameObject DeactivatedCubes;
    public GameObject EndGameButtons, ExitPanel, Buttons;

    public Text LivesText;

    public Transform DeathStars;

    public static Mode IsMode { get; set; }

    private void Start()
    {
        _adsManager = FindObjectOfType<AdsManager>();
        _adsManager.InitializeAdvertisements();
    }

    private void Update()
    {
        ShowExitPanel();
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
        throw new System.NotImplementedException();
    }

    public void Cube_OnJumped()
    {
        //if (_lives >= 1)
        //{
        //    LivesText.gameObject.SetActive(true);
        //}

        //_lives--;
        throw new System.NotImplementedException();
    }

    public void Cube_OnFellGround()
    {
        if (_lives == 0)
        {
            DisplayEndGameButtons();
            _collision.OnFellGround -= Cube_OnFellGround;
        }
    }

    public void SubscribeOnEvent()
    {
        _collision.OnFellGround += Cube_OnFellGround;
    }

    public void IntroduceCube(GameObject cube)
    {
        _collision = cube.GetComponent<CollisionHandler>();
        var uiEvent = _canvas.GetComponent<UIEvent>();
        uiEvent.IntroduceCube(_collision);
        SubscribeOnEvent();
    }
}