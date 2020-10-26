using UnityEngine;

public class SpawnCubes : MonoBehaviour
{
    public GameObject Cube;
    public GameObject MainCube;
    public GameObject CubesParent;

    private JumpClickController jumpClickController;

    private readonly int layerCube = 9;

    public void GetNewCube()
    {
        var cube = Instantiate(Cube, new Vector3(-4f, -4.08f, 5f),
            Quaternion.Euler(new Vector3(0, 60, 0)),
            CubesParent.transform);
        cube.layer = layerCube;
        cube.GetComponent<MeshRenderer>().material = MainCube.GetComponent<MeshRenderer>().material;
        jumpClickController.GetControl(cube);
    }

    private void Start()
    {
        jumpClickController = gameObject.GetComponentInChildren<JumpClickController>();
    }
}