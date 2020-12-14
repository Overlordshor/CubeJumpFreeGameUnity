using UnityEngine;

public class Cube : MonoBehaviour
{
    public GameObject BrokenCube;
    public AudioClip audioPass, audioCrash, audioHit, audioSqueeze;

    public float ForceJump { get; private set; }

    private readonly int layerGround = 8;
    private readonly int layerCube = 9;

    private bool jumped = false;
    private bool transferControl = false;

    private readonly float originalScaleCube = 0.6f;
    private readonly float compressionScaleCube = 0.3f;
    private Vector3 forceCompression = new Vector3(0, 0.01f, 0f);

    private Transform transformCube;
    private Rigidbody rigidbodyCube;
    private Game game;
    private AudioSource audioSource;
    private bool playedAudioSqueeze = false;

    //private Color color;

    private bool isGround = true;

    public void PlayAudioSqueeze(bool clickDetected)
    {
        if (clickDetected && !playedAudioSqueeze)
        {
            PlayAudio(audioSqueeze);
            playedAudioSqueeze = true;
        }
    }

    public void Squeeze(bool clickDetected)
    {
        if (clickDetected && transformCube.localScale.y > compressionScaleCube && isGround)
        {
            transformCube.localScale -= forceCompression;
        }
        else if (transformCube.localScale.y < originalScaleCube)
        {
            transformCube.localScale += forceCompression * 2;
        }
    }

    public void Jump(float pushtime)
    {
        if (isGround)
        {
            ForceJump = GetForces(pushtime);

            rigidbodyCube.AddRelativeForce(transform.right * -ForceJump);
            rigidbodyCube.AddRelativeForce(transform.up * ForceJump * 2.5f);

            isGround = false;
            jumped = true;
            game.LoseJumpAttempt();

            PlayAudio(audioPass);
        }
    }

    private void Start()
    {
        transformCube = gameObject.GetComponent<Transform>();
        rigidbodyCube = gameObject.GetComponent<Rigidbody>();
        game = FindObjectOfType<Game>();
        audioSource = GetComponent<AudioSource>();

        //SetRandomColor(); temporarily disabled

        game.AppearedNewCube = false;
    }

    private void PlayAudio(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.Play();
    }

    private void SetRandomColor()
    {
        GetComponent<MeshRenderer>().material.color = new Color(GetRandomFloat(), GetRandomFloat(), GetRandomFloat());
        //color = GetComponent<MeshRenderer>().material.color;
    }

    private float GetRandomFloat()
    {
        float random = Random.Range(0f, 1f);
        return random;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == layerGround)
        {
            isGround = true;
            if (jumped)
            {
                BreakDown();
                if (!transferControl)
                {
                    game.DisplayButtons();
                }
            }
        }
        if (collision.gameObject.layer == layerCube)
        {
            if (game.JumpAttempt == 0)
            {
                game.GetReward();
                game.CreateNewCube();
            }
            if (!transferControl)
            {
                PlayAudio(audioHit);
            }
            transferControl = true;
        }
    }

    private void BreakDown()
    {
        game.PlayAudioBrokenBox();
        var brokenCube = Instantiate(BrokenCube,
                            gameObject.transform.position,
                            Quaternion.identity,
                            game.DeactivatedCubes.transform);
        brokenCube.GetComponent<BrokenCubes>().PassMaterial(gameObject.GetComponent<MeshRenderer>().material);

        gameObject.transform.parent = game.DeactivatedCubes.transform;
        gameObject.SetActive(false);
    }
}