using UnityEngine;

public class CubeJump : MonoBehaviour
{
    private float originalScaleCube = 1f;
    private float compressionScaleCube = 0.3f;
    private Vector3 forceCompression = new Vector3(0, 0.01f, 0f);

    private Transform transformCube;
    private Rigidbody rigidbodyCube;
    private GameChecker gameChecker;

    public void Squeeze(bool clickDetected)
    {
        if (clickDetected && transformCube.localScale.y > compressionScaleCube && gameChecker.IsGround)
        {
            transformCube.localScale -= forceCompression;
        }
        else if (transformCube.localScale.y < originalScaleCube)
        {
            transformCube.localScale += forceCompression * 2;
        }
    }

    public void Jump(float pushtime)
    {
        if (gameChecker.IsGround)
        {
            var forceJump = GetForces(pushtime);

            rigidbodyCube.AddRelativeForce(transform.right * -forceJump);
            rigidbodyCube.AddRelativeForce(transform.up * forceJump * 2.5f);

            gameChecker.IsGround = false;
            gameChecker.LoseJumpAttempt();
        }
    }

    private void Start()
    {
        transformCube = gameObject.GetComponent<Transform>();
        rigidbodyCube = gameObject.GetComponent<Rigidbody>();
        gameChecker = gameObject.GetComponent<GameChecker>();
    }

    private float GetForces(float pushTime)
    {
        float force;
        if (pushTime < 3f)
        {
            force = 190f * pushTime;
            if (force < 60f)
            {
                force = 60f;
            }
        }
        else
        {
            force = 300f;
        }
        return force;
    }
}