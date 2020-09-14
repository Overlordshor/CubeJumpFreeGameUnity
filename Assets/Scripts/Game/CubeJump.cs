using UnityEngine;

public class CubeJump : MonoBehaviour
{
    public GameObject Cube;

    private bool clickDetected;
    private float originalScaleCube;
    private float compressionScaleCube;
    private Vector3 forceCompression;
    private float startTime;
    private float forceJump;
    private Rigidbody rigidbodyCube;

    private void Start()
    {
        clickDetected = false;
        originalScaleCube = 1f;
        compressionScaleCube = 0.3f;
        forceCompression = new Vector3(0, 0.01f, 0f);
        rigidbodyCube = Cube.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (clickDetected && Cube.transform.localScale.y > compressionScaleCube)
        {
            Cube.transform.localScale -= forceCompression;
        }
        else if (Cube.transform.localScale.y < originalScaleCube)
        {
            Cube.transform.localScale += forceCompression * 2;
        }
    }

    private void OnMouseDown()
    {
        clickDetected = true;

        startTime = Time.time;
    }

    private void OnMouseUp()
    {
        clickDetected = false;

        forceJump = GetForces();

        rigidbodyCube.AddRelativeForce(Cube.transform.right * -forceJump);
        rigidbodyCube.AddRelativeForce(Cube.transform.up * forceJump * 2.5f);
    }

    private float GetForces()
    {
        var pushTime = Time.time - startTime;
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