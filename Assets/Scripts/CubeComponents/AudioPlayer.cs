using UnityEngine;

public class AudioPlayer : MonoBehaviour, ICubeEventComponent
{
    private CollisionHandler _collision;
    private Game _game;
    private JumpClickController _jumpClickController;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioPass, _audioCrash, _audioHit, _audioSqueeze;

    private void Start()
    {
        _collision = GetComponent<CollisionHandler>();
        _jumpClickController = FindObjectOfType<JumpClickController>();
        _game = FindObjectOfType<Game>();
        _audioSource = _game.GetComponent<AudioSource>(); // звук идёт после уничтожения кубов;

        SubscribeOnEvents();
    }

    public void Cube_OnCompressedCube()
    {
        _audioSource.PlayOneShot(_audioSqueeze);
        _jumpClickController.OnCompressedCube -= Cube_OnCompressedCube;
    }

    public void Cube_OnFellGround()
    {
        _audioSource.PlayOneShot(_audioCrash);
        _collision.OnFellGround -= Cube_OnFellGround;
    }

    public void Cube_OnHitCube()
    {
        _audioSource.PlayOneShot(_audioHit);
        _collision.OnHitCube -= Cube_OnHitCube;
    }

    public void Cube_OnJumped()
    {
        _audioSource.PlayOneShot(_audioPass);
        _collision.OnJumped -= Cube_OnJumped;
    }

    public void SubscribeOnEvents()
    {
        _collision.OnFellGround += Cube_OnFellGround;
        _collision.OnJumped += Cube_OnJumped;
        _collision.OnHitCube += Cube_OnHitCube;
        _jumpClickController.OnCompressedCube += Cube_OnCompressedCube;
    }
}