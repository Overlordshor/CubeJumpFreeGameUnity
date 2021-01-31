using UnityEngine;

public class SpawnCubes : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    private UIEvent _uIEvent;
    private JumpClickController _jumpClickController;
    private CollisionHandler _collision;

    private static int _countCubes;

    private void Start()
    {
        _jumpClickController = gameObject.GetComponentInChildren<JumpClickController>();
        _uIEvent = _canvas.GetComponentInChildren<UIEvent>();
    }

    public void GetCube(GameObject cubePrefab, GameObject mainCube)
    {
        var gameCube = Instantiate(cubePrefab, new Vector3(-4f, -4.08f, 5f),
            Quaternion.Euler(new Vector3(0, 60, 0)),
            mainCube.transform.parent);
        gameCube.layer = (int)Layer.Cube;
        gameCube.GetComponent<MeshRenderer>().material = mainCube.GetComponent<MeshRenderer>().material;

        _collision = gameCube.GetComponent<CollisionHandler>();
        _uIEvent.SubcribeCube(_collision);
        var reducer = gameCube.GetComponent<Reducer>();

        if (Game.IsMode == Mode.Classic)
        {
            if (reducer != null)
            {
                reducer.enabled = false;
            }
        }

        //var cube = gameCube.GetComponent<CubeSqueezer>();
        //cube.SetCompressionScale(_countCubes);
        //gameCube.transform.localScale -= new Vector3(cube.ReducedScale, cube.ReducedScale, cube.ReducedScale) * _countCubes;
        //_countCubes++;

        _jumpClickController.GetControl(gameCube);
    }
}