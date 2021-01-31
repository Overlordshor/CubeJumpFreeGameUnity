using UnityEngine;

public delegate void OnFellGroundDelegate();

public delegate void OnJumpedDelegate();

public delegate void OnHitCubeDelegate();

public class CollisionHandler : MonoBehaviour
{
    private Cube _cube;
    [HideInInspector] public Collision CollisionCube;

    public event OnFellGroundDelegate OnFellGround;

    public event OnJumpedDelegate OnJumped;

    public event OnHitCubeDelegate OnHitCube;

    private void Start()
    {
        _cube = GetComponent<Cube>();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == (int)Layer.Ground)
        {
            OnJumped();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)Layer.Ground)
        {
            if (_cube.IsJumped)
            {
                OnFellGround();
            }
        }
        else if (collision.gameObject.layer == (int)Layer.Cube)
        {
            CollisionCube = collision;
            OnHitCube();
        }
    }
}