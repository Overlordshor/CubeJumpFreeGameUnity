using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject MainCube;
    public CubeForSale[] cubesForSale;

    [SerializeField]
    private Text cubeName;

    [SerializeField]
    private Text goldCost;

    public void UpdateDisplayUI(CubeData cubeData)
    {
        cubeName.text = cubeData.CubeName;
        Language.PrintAnyLanguage(goldCost, cubeData.Cost + " GOLD", cubeData.Cost + " ЗОЛОТЫХ");
    }

    public void SetMaterialCube(CubeForSale cube)
    {
        MainCube.GetComponent<MeshRenderer>().material = cube.Material;
        PlayerPrefs.SetString("Skin", cube.gameObject.name);
    }

    public CubeForSale GetSelectCube()
    {
        CubeForSale cubeSelect = new CubeForSale();
        foreach (var cube in cubesForSale)
        {
            if (cube.Select)
            {
                cubeSelect = cube;
                break;
            }
        }
        return cubeSelect;
    }

    public void SelectCube(GameObject gameObject)
    {
        foreach (var cube in cubesForSale)
        {
            if (cube.name == gameObject.name)
            {
                cube.Select = true;
                continue;
            }
            cube.Select = false;
        }
    }

    private void Start()
    {
        var transformCubesForSale = transform.Find("Cubes");
        cubesForSale = new CubeForSale[transformCubesForSale.childCount];
        for (int i = 0; i < transformCubesForSale.childCount; i++)
        {
            cubesForSale[i] = transformCubesForSale.GetChild(i).GetComponent<CubeForSale>();
        }
    }
}