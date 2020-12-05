using System;
using UnityEngine;

public class CubeForSale : MonoBehaviour
{
    public int Cost { get; set; }
    public bool Select { get; set; }
    public bool Open { get; set; }
    public Material Material { get; private set; }

    private GameObject acceptButton;
    private new AudioSource audio;
    private Vector3 originalScale;
    private ButtonAccept buttonAccept;
    private Shop shop;
    private readonly string keyOpen = "Open";

    private void Start()
    {
        acceptButton = transform.parent.transform.parent.Find("AcceptButton").gameObject;
        buttonAccept = acceptButton.GetComponent<ButtonAccept>();
        audio = GetComponentInParent<AudioSource>();
        Material = GetComponent<MeshRenderer>().material;
        shop = transform.parent.transform.parent.GetComponent<Shop>();
        originalScale = transform.localScale;
        Cost = 200;

        if (!PlayerPrefs.HasKey("CubeStar") && gameObject.name == "CubeStar")
        {
            PlayerPrefs.SetString(gameObject.name, keyOpen);
        }
        Open = PlayerPrefs.GetString(gameObject.name) == keyOpen;

        Select = false;
    }

    private void OnMouseUpAsButton()
    {
        audio.Play();
        acceptButton.SetActive(true);
        if (!Select)
        {
            buttonAccept.GetButtonImage(this);

            transform.localScale += new Vector3(50f, 50f, 50f);

            shop.SelectCube(gameObject);
        }
    }

    private void Update()
    {
        if (!Select)
        {
            if (transform.localScale != originalScale)
            {
                transform.localScale -= new Vector3(1f, 1f, 1f); ;
            }
        }
    }
}