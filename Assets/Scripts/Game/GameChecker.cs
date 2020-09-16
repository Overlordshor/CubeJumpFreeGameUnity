using UnityEngine;
using UnityEngine.SceneManagement;

public class GameChecker : MonoBehaviour
{
    public bool IsGround { get; set; } = true;

    private int jumpAttempt = 1;
    private int layerGround = 8;
    private int layerCube = 9;
    private bool successJump = false;
    private bool gaveNewCube = false;

    public void LoseJumpAttempt()
    {
        jumpAttempt--;
        if (!gameObject.activeSelf)
        {
            SceneManager.LoadScene("Main");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == layerGround)
        {
            IsGround = true;
            if (jumpAttempt == 0 || successJump)
            {
                gameObject.SetActive(false);
            }
        }
        if (collision.gameObject.layer == layerCube)
        {
            successJump = true;
            if (!gaveNewCube)
            {
                FindObjectOfType<SpawnCubes>().GetNewCube();
                gaveNewCube = true;
            }
        }
    }
}