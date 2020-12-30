using UnityEngine;

[CreateAssetMenu(fileName = "New CubeData", menuName = "CubeData", order = 51)]
public class CubeData : ScriptableObject
{
    [SerializeField]
    private string cubeNameEnglish;

    [SerializeField]
    private string cubeNameRussian;

    [SerializeField]
    private int cost;

    [SerializeField]
    private Material material;

    [SerializeField]
    private bool open;

    public int Cost => cost;
    public Material Material => material;

    public bool Open
    {
        get
        {
            if (PlayerPrefs.GetString(cubeNameEnglish) == Keys.Open)
            {
                open = true;
            }
            return open;
        }
    }

    public string CubeName
    {
        get
        {
            if (PlayerPrefs.GetString(Keys.Language) == "Russian")
            {
                return cubeNameRussian;
            }
            else
            {
                return cubeNameEnglish;
            }
        }
    }
}