using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeJump : MonoBehaviour
{
    public GameObject Cube;
    private bool clickDetected = false;
    private float speedСompression = 0.05f;
    private Vector3 originalScaleCube = new Vector3(1f, 1f, 1f);
    private Vector3 compressionScaleCube = new Vector3(1f, 0.2f, 1f);

    private void FixedUpdate()
    {
        if (clickDetected && Cube.transform.localScale != compressionScaleCube)
        {
            Cube.transform.localScale -= new Vector3(0, speedСompression, 0);
        }
        else if (Cube.transform.localScale != originalScaleCube)
        {
            Cube.transform.localScale += new Vector3(0, speedСompression, 0);
        }
    }

    private void OnMouseDown()
    {
        clickDetected = true;
    }

    private void OnMouseUp()
    {
        clickDetected = false;
    }
}