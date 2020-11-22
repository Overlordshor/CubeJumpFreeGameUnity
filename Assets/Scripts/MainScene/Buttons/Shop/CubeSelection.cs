using UnityEngine;

public class CubeSelection : MonoBehaviour
{
    public GameObject Cubes, MainCube;
    private new AudioSource audio;
    private Material selectMaterial;

    private void Start()
    {
        audio = GetComponentInParent<AudioSource>();
    }

    private void OnMouseUpAsButton()
    {
        audio.Play();

        selectMaterial = gameObject.GetComponent<MeshRenderer>().material;
        PlayerPrefs.SetString("Skin", selectMaterial.name);
        var originalScale = transform.localScale;
        transform.localScale += new Vector3(50f, 50f, 50f);
    }
}