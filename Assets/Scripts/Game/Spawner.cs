using UnityEngine;

public class Spawner : MonoBehaviour, ICubeEventComponent
{
    private SpawnCubes _spawnCubes;
    private CollisionHandler _collision;

    private void Start()
    {
        _spawnCubes = FindObjectOfType<SpawnCubes>();
        _collision = GetComponent<CollisionHandler>();
        SubscribeOnEvent();
    }

    public void SubscribeOnEvent()
    {
        _collision.OnHitCube += Cube_OnHitCube;
        _collision.OnHitCube += Cube_OnFellGround;
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
        _spawnCubes.GetCube(gameObject, gameObject);
        _collision.OnHitCube -= Cube_OnHitCube;
    }

    public void Cube_OnJumped()
    {
        throw new System.NotImplementedException();
    }
}