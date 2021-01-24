using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool _isJumped = false;
    private bool _isGround = false;
    private bool _isTransferControl = false;
    private bool _playedAudioSqueeze = false;

    private float _originalScaleCube = 0.6f;
    private float _compressionScaleCube = 0.3f;

    private Vector3 _forceCompression = new Vector3(0, 0.01f, 0f);

    private Transform _transformCube;
    private Rigidbody _rigidbodyCube;
    private Game _game;
    private AudioSource _audioSource;

    private MeshRenderer _meshRenderer;
    private Color _colorDefault;

    [SerializeField] private AudioClip _audioPass, _audioCrash, _audioHit, _audioSqueeze;
    [SerializeField] private GameObject _brokenCube, _expolosion;

    public float ForceJump { get; private set; }
    public float ReducedScale { get; private set; }

    private void Awake()
    {
        ReducedScale = 0.01f;
        _game = FindObjectOfType<Game>();
    }

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _colorDefault = _meshRenderer.material.color;

        _transformCube = gameObject.GetComponent<Transform>();
        _rigidbodyCube = gameObject.GetComponent<Rigidbody>();

        _audioSource = GetComponent<AudioSource>();

        _game.AppearedNewCube = false;
    }

    public void SetCompressionScale(int index)
    {
        if (_game.IsMode == Mode.Reduction)
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

    private void PlayAudio(AudioClip audio)
    {
        _audioSource.clip = audio;
        _audioSource.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)Layer.Ground)
        {
            _isGround = true;

            if (_isJumped)
            {
                BreakDown();
                if (!_isTransferControl)
                {
                    _game.DisplayButtons();
                }
            }
        }
        if (collision.gameObject.layer == (int)Layer.Cube)
        {
            if (!_isTransferControl && _isJumped)
            {
                if (transform.position.x >= collision.transform.position.x - collision.transform.position.x * 20 / 100 &&
                    transform.position.x <= collision.transform.position.x + collision.transform.position.x * 20 / 100)
                {
                    _rigidbodyCube.freezeRotation = true;
                    _meshRenderer.material.color = new Color(_colorDefault.r / 2, 1f, _colorDefault.b / 2, 0.1f); // green
                }
                else
                {
                    _rigidbodyCube.freezeRotation = false;
                    _meshRenderer.material.color = new Color(1f, _colorDefault.g / 2, _colorDefault.b / 2, 0.1f); // red
                }

                Invoke(nameof(ResetMaterial), 1f);

                _game.GetReward();
                _game.CreateNewCube();
                PlayAudio(_audioHit);
                _isTransferControl = true;
            }
        }
    }

    private void ResetMaterial()
    {
        _meshRenderer.material.color = _colorDefault;
    }

    private void BreakDown()
    {
        _game.PlayAudioBrokenBox();
        var brokenCube = Instantiate(_brokenCube,
                            gameObject.transform.position,
                            Quaternion.identity,
                            _game.DeactivatedCubes.transform);
        brokenCube.GetComponent<BrokenCubes>().PassMaterial(gameObject.GetComponent<MeshRenderer>().material);
        Instantiate(_expolosion,
                            gameObject.transform.position,
                            Quaternion.identity, brokenCube.transform);

        gameObject.transform.parent = _game.DeactivatedCubes.transform;
        gameObject.SetActive(false);
    }

    public void PlayAudioSqueeze(bool clickDetected)
    {
        if (clickDetected && !_playedAudioSqueeze)
        {
            PlayAudio(_audioSqueeze);
            _playedAudioSqueeze = true;
        }
    }

    public void Squeeze(bool clickDetected)
    {
        if (clickDetected && _transformCube.localScale.y > _compressionScaleCube && _isGround)
        {
            _transformCube.localScale -= _forceCompression;
        }
        else if (_transformCube.localScale.y < _originalScaleCube)
        {
            _transformCube.localScale += _forceCompression * 2;
        }
    }

    public void Jump(float pushtime)
    {
        if (_isGround)
        {
            ForceJump = GetForces(pushtime);

            _rigidbodyCube.AddRelativeForce(transform.right * -ForceJump);
            _rigidbodyCube.AddRelativeForce(transform.up * ForceJump * 2.5f);

            _isGround = false;
            _isJumped = true;
            _game.LoseJumpAttempt();

            PlayAudio(_audioPass);
        }
    }

    public float GetForces(float pushTime)
    {
        if (pushTime < 0.07f)
        {
            pushTime = 0.07f;
        }
        float force;
        force = 370f * pushTime;
        if (force > 300f)
        {
            force = 300f;
        }

        return force;
    }
}