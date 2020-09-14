using UnityEngine;

public class JumpClickController : MonoBehaviour
{
    public GameObject Cube;

    private bool clickDetected;
    private float startTime;
    private CubeJump gameCube;

    private void Start()
    {
        gameCube = Cube.GetComponent<CubeJump>();
        clickDetected = false;
    }

    private void FixedUpdate()
    {
        gameCube.Squeeze(clickDetected);
    }

    private void OnMouseDown()
    {
        clickDetected = true;

        startTime = Time.time;
    }

    private void OnMouseUp()
    {
        clickDetected = false;

        var pushTime = Time.time - startTime;
        gameCube.Jump(pushTime);
    }
}