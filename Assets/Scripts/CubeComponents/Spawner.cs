using UnityEngine;

public class Spawner : MonoBehaviour, ICubeEventComponent
{
    private SpawnCubes _spawnCubes;
    private CollisionHandler _collision;
    private bool isPossible = false;

    private void Start()
    {
        _spawnCubes = FindObjectOfType<SpawnCubes>();
        _collision = GetComponent<CollisionHandler>();
        SubscribeOnEvents();
    }

    public void SubscribeOnEvents()
    {
        _collision.OnHitCube += Cube_OnHitCube;
        _collision.OnHitCube += Cube_OnFellGround;
        _collision.OnJumped += Cube_OnJumped;
    }

    public void Cube_OnCompressedCube()
    {
        throw new System.NotImplementedException();
    }

    public void Cube_OnFellGround()
    {
        _collision.OnHitCube -= Cube_OnHitCube;
    }

    public void Cube_OnHitCube()
    {
        if (isPossible)
        {
            _spawnCubes.GetCube(gameObject, gameObject);
            _collision.OnHitCube -= Cube_OnHitCube;
        }
    }

    public void Cube_OnJumped()
    {
        isPossible = true;
        _collision.OnJumped -= Cube_OnJumped;
    }
}