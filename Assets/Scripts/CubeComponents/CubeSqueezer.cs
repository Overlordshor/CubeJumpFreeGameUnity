using UnityEngine;

public class CubeSqueezer : MonoBehaviour, ICubeEventComponent
{
    private Cube _cube;
    private JumpClickController _jumpClickController;
    private CollisionHandler _collision;
    private Game _game;

    private bool _isStartSqueeze = false;

    private float _originalScaleCube = 0.6f;
    private float _compressionScaleCube = 0.3f;
    private Vector3 _forceCompression = new Vector3(0, 0.01f, 0f);

    public float ReducedScale { get; set; }

    private void Awake()
    {
        ReducedScale = 0.01f;
    }

    private void Start()
    {
        _cube = GetComponent<Cube>();
        _collision = GetComponent<CollisionHandler>();
        _jumpClickController = FindObjectOfType<JumpClickController>();
        _game = FindObjectOfType<Game>();

        SubscribeOnEvents();
    }

    private void FixedUpdate()
    {
        if (_isStartSqueeze)
        {
            Squeeze();
        }
    }

    private void Squeeze()
    {
        if (_jumpClickController.IsClickDetected && _cube.IsGround && transform.localScale.y > _compressionScaleCube)
        {
            transform.localScale -= _forceCompression;
        }
        else if (transform.localScale.y < _originalScaleCube)
        {
            transform.localScale += _forceCompression * 2;
        }
    }

    public void SetCompressionScale(int index)
    {
        if (Game.IsMode == Mode.Reduction)
        {
            if (_originalScaleCube > ReducedScale)
            {
                _originalScaleCube -= ReducedScale * index;

                if (_compressionScaleCube > ReducedScale)
                {
                    _compressionScaleCube -= ReducedScale * index;
                }
            }
        }
    }

    public void Cube_OnCompressedCube()
    {
        _isStartSqueeze = true;
    }

    public void Cube_OnHitCube()
    {
        _isStartSqueeze = false;
        _jumpClickController.OnCompressedCube -= Cube_OnCompressedCube;
    }

    public void Cube_OnJumped()
    {
        _jumpClickController.OnCompressedCube -= Cube_OnCompressedCube;
    }

    public void Cube_OnFellGround()
    {
        _jumpClickController.OnCompressedCube -= Cube_OnCompressedCube;
        _collision.OnHitCube -= Cube_OnHitCube;
        _collision.OnJumped -= Cube_OnJumped;
        _collision.OnFellGround -= Cube_OnFellGround;
    }

    public void SubscribeOnEvents()
    {
        _jumpClickController.OnCompressedCube += Cube_OnCompressedCube;
        _collision.OnHitCube += Cube_OnHitCube;
        _collision.OnJumped += Cube_OnJumped;
        _collision.OnFellGround += Cube_OnFellGround;
    }
}