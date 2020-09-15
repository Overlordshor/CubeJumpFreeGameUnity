using UnityEngine;

public class CubeJump : MonoBehaviour
{
    private float originalScaleCube = 1f;
    private float compressionScaleCube = 0.3f;
    private Vector3 forceCompression = new Vector3(0, 0.01f, 0f);

    private Transform transformCube;
    private Rigidbody rigidbodyCube;

    private bool isGrounded = true;
    private int layerGround = 8;
    private int layerCube = 9;

    public void Squeeze(bool clickDetected)
    {
        if (clickDetected && transformCube.localScale.y > compressionScaleCube && isGrounded)
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
        if (isGrounded)
        {
            var forceJump = GetForces(pushtime);

            rigidbodyCube.AddRelativeForce(transform.right * -forceJump);
            rigidbodyCube.AddRelativeForce(transform.up * forceJump * 2.5f);

            isGrounded = false;
        }
    }

    private void Start()
    {
        transformCube = gameObject.GetComponent<Transform>();
        rigidbodyCube = gameObject.GetComponent<Rigidbody>();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == layerGround)
        {
            isGrounded = true;
        }
        if (collision.gameObject.layer == layerCube)
        {
        }
    }
}