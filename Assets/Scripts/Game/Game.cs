using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject DeactivatedCubes;
    public GameObject CubesTower;

    private SpawnCubes cubeSpawner;
    private Score score;

    public int JumpAttempt { get; set; } = 1;

    public bool AppearedNewCube { get; set; } = false;

    public void DisplayText()
    {
        if (JumpAttempt == 0)
        {
            GetComponentInChildren<JumpClickController>().GetFinalText();
        }
    }

    public void CreateNewCube(bool passedControl)
    {
        if (!AppearedNewCube && !passedControl)
        {
            cubeSpawner.GetNewCube();
            JumpAttempt++;
            AppearedNewCube = true;

            PassHeightTower();
            score.Add();
        }
    }

    public void LoseJumpAttempt()
    {
        JumpAttempt--;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }

    private void Start()
    {
        cubeSpawner = GetComponent<SpawnCubes>();
        score = GetComponent<Score>();
    }

    private void PassHeightTower()
    {
        var heigtTower = CubesTower.transform.childCount;
        score.RefreshBuff(heigtTower);
    }
}