using System.Collections;
using UnityEngine;

public class SpawnCubes : MonoBehaviour
{
    public GameObject Cube;
    public GameObject MainCube;
    public GameObject CubesParent;

    private void Start()
    {
        StartCoroutine("newCube");
    }
    public IEnumerator newCube()
    {
        yield return new WaitForSeconds(MainCube.GetComponent<Animation>().clip.length + 0.5f);
        MainCube.AddComponent<Rigidbody>();

        var cube = Instantiate(Cube, new Vector3(4f, -4.08f, 5f), Quaternion.Euler(new Vector3(0, 60, 0)));
        cube.transform.parent = CubesParent.transform;
    }
}