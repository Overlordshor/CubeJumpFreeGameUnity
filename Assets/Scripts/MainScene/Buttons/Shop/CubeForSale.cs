using UnityEngine;

public class CubeForSale : MonoBehaviour
{
    [SerializeField]
    private CubeData cubeData;

    [SerializeField]
    private GameEvent onCubeSelected;

    public bool IsSelect { get; set; }

    private GameObject acceptButton;
    private new AudioSource audio;
    private Vector3 originalScale;
    private ButtonAccept buttonAccept;
    private Shop shop;

    private void Start()
    {
        acceptButton = transform.parent.transform.parent.Find("AcceptButton").gameObject;
        buttonAccept = acceptButton.GetComponent<ButtonAccept>();
        audio = GetComponentInParent<AudioSource>();
        shop = transform.parent.transform.parent.GetComponent<Shop>();
        originalScale = transform.localScale;

        if (!PlayerPrefs.HasKey("4") && cubeData.CubeName == "CubeStar")
        {
            PlayerPrefs.SetString("4", Keys.Open);
        }

        IsSelect = false;
    }

    private void OnMouseUpAsButton()
    {
        onCubeSelected.Raise();

        audio.Play();
        acceptButton.SetActive(true);
        if (!IsSelect)
        {
            buttonAccept.GetButtonImage(cubeData);

            transform.localScale += new Vector3(50f, 50f, 50f);

            shop.SelectCube(gameObject);
        }
    }

    private void Update()
    {
        if (!IsSelect)
        {
            if (transform.localScale != originalScale)
            {
                transform.localScale -= new Vector3(1f, 1f, 1f); ;
            }
        }
    }
}