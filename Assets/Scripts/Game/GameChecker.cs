using UnityEngine;

public class GameChecker : MonoBehaviour
{
    public bool IsGround { get; set; } = true;
    public int JumpAttempt { get; set; } = 1;

    private int layerGround = 8;
    private int layerCube = 9;
    private bool successJump = false;
    private bool gaveNewCube = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == layerGround)
        {
            IsGround = true;
            if (JumpAttempt == 0 || successJump)
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