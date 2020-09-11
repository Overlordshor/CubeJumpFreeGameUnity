using UnityEngine;

public class SpawnCubes : MonoBehaviour
{
    public GameObject Cube;

    private void Start()
    {
        Instantiate(Cube, new Vector3(4f, -4.08f, 5f), Quaternion.Euler(new Vector3(0, 60, 0)));
    }
}