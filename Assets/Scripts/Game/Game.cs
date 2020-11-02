using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject DeactivatedCubes;
    public GameObject CubesTower;

    private SpawnCubes cubeSpawner;
    private Score score;
    private Coin coin;
    private AudioSource audioBrokenBox;

    public int JumpAttempt { get; set; } = 1;

    public bool AppearedNewCube { get; set; } = false;

    public void DisplayText()
    {
        if (JumpAttempt == 0)
        {
            GetComponentInChildren<JumpClickController>().GetFinalText();
        }
    }

    public void CreateNewCube()
    {
        if (!AppearedNewCube)
        {
            cubeSpawner.GetNewCube();
            JumpAttempt++;
            AppearedNewCube = true;

            PassHeightTower();
            score.PrintBuffOnScreen();
            score.Add();
            coin.Add();
        }
    }

    public int GetHeightTower()
    {
        return CubesTower.transform.childCount;
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

    private void Start()
    {
        cubeSpawner = GetComponent<SpawnCubes>();
        score = GetComponent<Score>();
        coin = GetComponent<Coin>();
        audioBrokenBox = GetComponent<AudioSource>();
    }

    private void PassHeightTower()
    {
        var heigtTower = CubesTower.transform.childCount;
        score.RefreshBuff(heigtTower);
    }
}