using UnityEngine;

public class Cube : MonoBehaviour
{
    public GameObject BrokenCube;

    private int layerGround = 8;
    private int layerCube = 9;

    private bool jumped = false;
    private bool passedControl = false;

    private float originalScaleCube = 0.6f;
    private float compressionScaleCube = 0.3f;
    private Vector3 forceCompression = new Vector3(0, 0.01f, 0f);

    private Transform transformCube;
    private Rigidbody rigidbodyCube;
    private Game game;

    private bool isGround { get; set; } = true;

    public void Squeeze(bool clickDetected)
    {
        if (clickDetected && transformCube.localScale.y > compressionScaleCube && isGround)
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
        if (isGround)
        {
            var forceJump = GetForces(pushtime);

            rigidbodyCube.AddRelativeForce(transform.right * -forceJump);
            rigidbodyCube.AddRelativeForce(transform.up * forceJump * 2.5f);

            isGround = false;
            jumped = true;
            game.LoseJumpAttempt();
        }
    }

    private void Start()
    {
        transformCube = gameObject.GetComponent<Transform>();
        rigidbodyCube = gameObject.GetComponent<Rigidbody>();
        game = FindObjectOfType<Game>();

        SetRandomColor();

        game.AppearedNewCube = false;
    }

    private void SetRandomColor()
    {
        GetComponent<MeshRenderer>().material.color = new Color(GetRandomFloat(), GetRandomFloat(), GetRandomFloat());
    }

    private float GetRandomFloat()
    {
        float v = Random.Range(0f, 1f);
        return v;
    }

    private float GetForces(float pushTime)
    {
        float force;
        force = 370f * pushTime;
        if (force > 300f)
        {
            force = 300f;
        }
        return force;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == layerGround)
        {
            isGround = true;
            if (jumped)
            {
                BreakDown();
                if (!passedControl)
                {
                    game.DisplayText();
                }
            }
        }
        if (collision.gameObject.layer == layerCube)
        {
            game.CreateNewCube(passedControl);
            passedControl = true;
        }
    }

    private void BreakDown()
    {
        Instantiate(BrokenCube,
                            gameObject.transform.position,
                            Quaternion.identity,
                            gameObject.transform.parent);
        gameObject.SetActive(false);
        gameObject.transform.parent = game.DeactivatedCubes.transform;
    }
}