using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject PlayButton, SocialGroupBar;
    public GameObject MainCube;

    public void SetMaterialCube(CubeSelection cube)
    {
        MainCube.GetComponent<MeshRenderer>().material = cube.SelectMaterial;
        PlayerPrefs.SetString("Skin", cube.gameObject.name);
    }

    private void OnEnable()
    {
        ToggleAllUI();
    }

    private void OnDisable()
    {
        ToggleAllUI();
    }

    private void ToggleUI(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    private void ToggleAllUI()
    {
        ToggleUI(PlayButton);
        ToggleUI(SocialGroupBar);
    }
}