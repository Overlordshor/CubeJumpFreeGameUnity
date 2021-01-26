using System.Collections;
using UnityEngine;

public class Breaker : MonoBehaviour
{
    private Cube _cube;
    [SerializeField] private GameObject _brokenCube, _deathStar, _expolosion;
    private Transform _deactivatedCubes;

    private void Start()
    {
        _cube = GetComponent<Cube>();
        _deactivatedCubes = _cube.Game.DeactivatedCubes.transform;
        _cube.OnFellGround += Break;
    }

    private void Break()
    {
        var brokenCube = Instantiate(_brokenCube,
                            gameObject.transform.position,
                            Quaternion.identity,
                            _deactivatedCubes);

        brokenCube.GetComponent<BrokenCubes>().PassMaterial(gameObject.GetComponent<MeshRenderer>().material);

        Instantiate(_deathStar, gameObject.transform.position,
                           Quaternion.identity,
                           _deactivatedCubes);
        Instantiate(_expolosion,
                            gameObject.transform.position,
                            Quaternion.identity,
                            brokenCube.transform);

        gameObject.transform.parent = _deactivatedCubes;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _cube.OnFellGround -= Break;
    }
}