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
    private bool isGround = false;

    private MeshRenderer meshRenderer;
    private Color colorDefault;

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
        meshRenderer = GetComponent<MeshRenderer>();
        colorDefault = meshRenderer.material.color;

        transformCube = gameObject.GetComponent<Transform>();
        rigidbodyCube = gameObject.GetComponent<Rigidbody>();
        game = FindObjectOfType<Game>();
        audioSource = GetComponent<AudioSource>();

        game.AppearedNewCube = false;
    }

    private void PlayAudio(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.Play();
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
            if (!transferControl && jumped)
            {
                if (transform.position.x >= collision.transform.position.x - collision.transform.position.x * 20 / 100 &&
                    transform.position.x <= collision.transform.position.x + collision.transform.position.x * 20 / 100)
                {
                    rigidbodyCube.freezeRotation = true;
                    meshRenderer.material.color = new Color(colorDefault.r, 1f, colorDefault.b, 0.1f); // green
                }
                else
                {
                    rigidbodyCube.freezeRotation = false;
                    meshRenderer.material.color = new Color(1f, colorDefault.g, colorDefault.b, 0.1f); // red
                }

                Invoke("ResetMaterial", 1f);

                game.GetReward();
                game.CreateNewCube();
                PlayAudio(audioHit);
                transferControl = true;
                // 1.44f 1.98f 0.2717f
            }
        }
    }

    private void ResetMaterial()
    {
        meshRenderer.material.color = colorDefault;
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