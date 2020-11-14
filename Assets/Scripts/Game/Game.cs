﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject DeactivatedCubes;
    public GameObject CubesTower;
    public GameObject EndGameButtons;

    private SpawnCubes cubeSpawner;
    private Score score;
    private Coin coin;
    private AudioSource audioBrokenBox;

    public int JumpAttempt { get; set; } = 1;

    public bool AppearedNewCube { get; set; } = false;

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

    public void PassHeightTower()
    {
        var heigtTower = CubesTower.transform.childCount;
        score.RefreshBuff(heigtTower);
        print(heigtTower);
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
    }

    private void Update()
    {
        Exit();
    }

    private void Exit()
    {
        if (Input.backButtonLeavesApp) // not work
        {
            PlayerPrefs.Save();
            Application.Quit();
        }
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PlayerPrefs.Save();
                Application.Quit();
            }
        }
    }
}