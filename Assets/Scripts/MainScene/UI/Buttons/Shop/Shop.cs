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

    private CubeData cubeData;

    private void Start()
    {
        var transformCubesForSale = transform.Find("Cubes");
        cubesForSale = new CubeForSale[transformCubesForSale.childCount];
        for (int i = 0; i < transformCubesForSale.childCount; i++)
        {
            cubesForSale[i] = transformCubesForSale.GetChild(i).GetComponent<CubeForSale>();
        }
    }

    public void UpdateDisplayUI(CubeData cubeData)
    {
        this.cubeData = cubeData;
        cubeName.text = cubeData.CubeName;
        Language.PrintAnyLanguage(goldCost, cubeData.Cost + " GOLD", cubeData.Cost + " ЗОЛОТЫХ");
    }

    public void SetMaterialCube(CubeData cube)
    {
        MainCube.GetComponent<MeshRenderer>().material = cube.Material;
        PlayerPrefs.SetString("Skin", cube.ID);
    }

    public CubeData GetSelectCube()
    {
        return cubeData;
    }

    public CubeForSale GetSelectCube1()
    {
        CubeForSale cubeSelect = new CubeForSale();
        foreach (var cube in cubesForSale)
        {
            if (cube.IsSelect)
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
                cube.IsSelect = true;
                continue;
            }
            cube.IsSelect = false;
        }
    }
}