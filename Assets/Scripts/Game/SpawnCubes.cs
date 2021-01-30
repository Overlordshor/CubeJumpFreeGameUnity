using UnityEngine;

public class SpawnCubes : MonoBehaviour
{
    private JumpClickController _jumpClickController;
    private Game _game;

    private static int _countCubes;

    private void Start()
    {
        _jumpClickController = gameObject.GetComponentInChildren<JumpClickController>();
        _game = GetComponent<Game>();
    }

    public void GetCube(GameObject cubePrefab, GameObject mainCube)
    {
        var gameCube = Instantiate(cubePrefab, new Vector3(-4f, -4.08f, 5f),
            Quaternion.Euler(new Vector3(0, 60, 0)),
            mainCube.transform.parent);
        gameCube.layer = (int)Layer.Cube;
        gameCube.GetComponent<MeshRenderer>().material = mainCube.GetComponent<MeshRenderer>().material;
        if (_game.IsMode == Mode.Reduction)
        {
            var cube = gameCube.GetComponent<CubeSqueezer>();
            cube.SetCompressionScale(_countCubes);
            gameCube.transform.localScale -= new Vector3(cube.ReducedScale, cube.ReducedScale, cube.ReducedScale) * _countCubes;
            _countCubes++;
        }

        _jumpClickController.GetControl(gameCube);
        _game.IntroduceCube(gameCube);
    }
}