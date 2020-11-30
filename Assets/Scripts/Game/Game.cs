using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class Game : MonoBehaviour
{
    public GameObject DeactivatedCubes;
    public GameObject CubesTower;
    public GameObject EndGameButtons, ExitPanel, Buttons;

    private SpawnCubes cubeSpawner;
    private Score score;
    private Coin coin;
    private AudioSource audioBrokenBox;

    private string placement = "video";

    public int JumpAttempt { get; set; } = 1;

    public bool AppearedNewCube { get; set; } = false;

    public void ShowAds()
    {
        if (Advertisement.IsReady(placement))
        {
            Advertisement.Show(placement);
        }
    }

    public void DisplayButtons()
    {
        if (JumpAttempt == 0)
        {
            ActivateButtonsEnd();
        }
    }

    public void CreateNewCube()
    {
        if (!AppearedNewCube)
        {
            cubeSpawner.GetNewCube();
            JumpAttempt++;
            AppearedNewCube = true;

            score.Add();
            coin.Add();
        }
    }

    public void LoseJumpAttempt()
    {
        JumpAttempt--;
    }

    public void Restart()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("Main");
    }

    public void PlayAudioBrokenBox()
    {
        audioBrokenBox.Play();
    }

    private void ActivateButtonsEnd()
    {
        EndGameButtons.SetActive(true);
    }

    private void Start()
    {
        cubeSpawner = GetComponent<SpawnCubes>();
        score = GetComponent<Score>();
        coin = GetComponent<Coin>();
        audioBrokenBox = GetComponent<AudioSource>();
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("3921519", false);
        }
    }

    private void Update()
    {
        ShowExitPanel();
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