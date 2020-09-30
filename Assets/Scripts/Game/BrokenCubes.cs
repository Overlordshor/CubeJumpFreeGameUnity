using UnityEngine;

public class BrokenCubes : MonoBehaviour
{
    public void GetColorCube(Color colorCube)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponentInChildren<MeshRenderer>().material.color = colorCube;
        }
    }

    private void Start()
    {
        Destroy(gameObject, 1.5f);
    }
}