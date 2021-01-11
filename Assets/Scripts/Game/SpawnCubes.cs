using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnCubes : MonoBehaviour
{
    private JumpClickController _jumpClickController;
    private int _countCubes;

    public GameObject Cube;
    public GameObject MainCube;
    public GameObject CubesParent;

    private void Start()
    {
        _jumpClickController = gameObject.GetComponentInChildren<JumpClickController>();
    }

    public void GetNewCube()
    {
        var gameCube = Instantiate(Cube, new Vector3(-4f, -4.08f, 5f),
            Quaternion.Euler(new Vector3(0, 60, 0)),
            CubesParent.transform);
        gameCube.layer = Keys.Layer.Cube;
        gameCube.GetComponent<MeshRenderer>().material = MainCube.GetComponent<MeshRenderer>().material;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            var cube = gameCube.GetComponent<Cube>();
            cube.SetCompressionScale(_countCubes);
            gameCube.transform.localScale -= new Vector3(cube.ReducedScale, cube.ReducedScale, cube.ReducedScale) * _countCubes;
            _countCubes++;
        }
        _jumpClickController.GetControl(gameCube);
    }
}