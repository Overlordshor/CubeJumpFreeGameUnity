using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class Game : MonoBehaviour
{
    private AdsManager _adsManager;

    [SerializeField] private GameObject _exitPanel, _buttons, _rules;

    public GameObject DeactivatedCubes;

    public Text LivesText;

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
                _exitPanel.SetActive(true);
                _buttons?.SetActive(false);
                _rules.SetActive(false);
            }
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _exitPanel.SetActive(true);
            _buttons?.SetActive(false);
            _rules.SetActive(false);
        }
#endif
    }
}