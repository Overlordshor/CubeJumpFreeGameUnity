using UnityEngine;

public class Reducer : MonoBehaviour, ICubeEventComponent
{
    private CollisionHandler _collision;
    private static int _index;

    private void Awake()
    {
        if (Game.IsMode == Mode.Reduction)
        {
            _collision = GetComponent<CollisionHandler>();
            var cube = GetComponent<CubeSqueezer>();

            cube.SetCompressionScale(_index);
            transform.localScale -= new Vector3(cube.ReducedScale, cube.ReducedScale, cube.ReducedScale) * _index;

            SubscribeOnEvents();
        }
    }

    private void OnDisable()
    {
        Destroy(this);
    }

    public void Cube_OnCompressedCube()
    {
        throw new System.NotImplementedException();
    }

    public void Cube_OnFellGround()
    {
        _index -= 2;
        _collision.OnFellGround -= Cube_OnFellGround;
    }

    public void Cube_OnHitCube()
    {
        _index++;
        _collision.OnHitCube -= Cube_OnHitCube;
    }

    public void Cube_OnJumped()
    {
        throw new System.NotImplementedException();
    }

    public void SubscribeOnEvents()
    {
        _collision.OnHitCube += Cube_OnHitCube;
        _collision.OnFellGround += Cube_OnFellGround;
    }
}