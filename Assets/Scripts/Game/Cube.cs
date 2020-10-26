using UnityEngine;

public class Cube : MonoBehaviour
{
    public GameObject BrokenCube;
    public AudioClip audioPass, audioCrash, audioHit, audioSqueeze;

    private readonly int layerGround = 8;
    private readonly int layerCube = 9;

    private bool jumped = false;
    private bool passedControl = false;

    private readonly float originalScaleCube = 0.6f;
    private readonly float compressionScaleCube = 0.3f;
    private Vector3 forceCompression = new Vector3(0, 0.01f, 0f);

    private Transform transformCube;
    private Rigidbody rigidbodyCube;
    private Game game;
    private AudioSource audioSource;
    private bool playedAudioSqueeze = false;

    private Color color;

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
            var forceJump = GetForces(pushtime);

            rigidbodyCube.AddRelativeForce(transform.right * -forceJump);
            rigidbodyCube.AddRelativeForce(transform.up * forceJump * 2.5f);

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
        color = GetComponent<MeshRenderer>().material.color;
    }

    private float GetRandomFloat()
    {
        float random = Random.Range(0f, 1f);
        return random;
    }

    private float GetForces(float pushTime)
    {
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
                if (!passedControl)
                {
                    game.DisplayText();
                }
            }
        }
        if (collision.gameObject.layer == layerCube)
        {
            game.CreateNewCube(passedControl);
            PlayAudio(audioHit);
            passedControl = true;
        }
    }

    private void BreakDown()
    {
        var brokenCube = Instantiate(BrokenCube,
                            gameObject.transform.position,
                            Quaternion.identity,
                            gameObject.transform.parent);
        brokenCube.GetComponent<BrokenCubes>().PassMaterial(gameObject.GetComponent<MeshRenderer>().material);
        gameObject.SetActive(false);
        gameObject.transform.parent = game.DeactivatedCubes.transform;
    }
}