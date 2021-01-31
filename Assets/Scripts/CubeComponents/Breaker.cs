using UnityEngine;

public class Breaker : MonoBehaviour, ICubeEventComponent
{
    private Game _game;
    private CollisionHandler _collision;
    [SerializeField] private GameObject _brokenCube, _deathStar, _expolosion;
    private Transform _deactivatedCubes;

    private void Start()
    {
        _game = FindObjectOfType<Game>();
        _collision = GetComponent<CollisionHandler>();
        _deactivatedCubes = _game.DeactivatedCubes.transform;
        SubscribeOnEvents();
    }

    public void Cube_OnCompressedCube()
    {
        throw new System.NotImplementedException();
    }

    public void Cube_OnHitCube()
    {
        throw new System.NotImplementedException();
    }

    public void Cube_OnJumped()
    {
        throw new System.NotImplementedException();
    }

    public void Cube_OnFellGround()
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

        _collision.OnFellGround -= Cube_OnFellGround;
    }

    public void SubscribeOnEvents()
    {
        _collision.OnFellGround += Cube_OnFellGround;
    }
}