using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private Cube _cube;
    private Game _game;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioBrokenBox;

    private void Start()
    {
        _cube = GetComponent<Cube>();
        _game = FindObjectOfType<Game>();
        _audioSource = _game.GetComponent<AudioSource>();
        _cube.OnFellGround += PlayAudioBreak;
    }

    private void PlayAudioBreak()
    {
        _audioSource.clip = _audioBrokenBox;
        _audioSource.Play();
        _cube.OnFellGround -= PlayAudioBreak;
    }
}