using UnityEngine;

public class CubeSelection : MonoBehaviour
{
    public bool Open { get; set; }
    public int Cost { get => costCube; private set => costCube = value; }
    public Material SelectMaterial { get => selectMaterial; set => selectMaterial = value; }

    private new AudioSource audio;
    private ButtonAccept buttonAccept;

    private Material selectMaterial;
    private Material cubeMaterial;

    private Vector3 originalScale;
    private bool clickedCube = false;

    private int costCube = 200;

    private void Start()
    {
        audio = GetComponentInParent<AudioSource>();
        cubeMaterial = GetComponent<MeshRenderer>().material;
        buttonAccept = FindObjectOfType<ButtonAccept>();
    }

    private void OnMouseUpAsButton()
    {
        audio.Play();

        if (!clickedCube)
        {
            buttonAccept.GetButtonImage(this);

            SelectMaterial = cubeMaterial;

            originalScale = transform.localScale;
            transform.localScale += new Vector3(50f, 50f, 50f); ;

            clickedCube = true;
        }
    }

    private void Update()
    {
        if (clickedCube && transform.localScale != originalScale)
        {
            transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
            if (transform.localScale == originalScale)
            {
                clickedCube = false;
            }
        }
    }
}