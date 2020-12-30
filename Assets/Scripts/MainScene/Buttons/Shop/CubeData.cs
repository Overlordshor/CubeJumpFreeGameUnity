using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New CubeData", menuName = "CubeData", order = 51)]
public class CubeData : ScriptableObject
{
    [SerializeField]
    private string cubeID;

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
    public string ID => cubeID;

    public bool Open
    {
        get
        {
            if (PlayerPrefs.GetString(cubeID) == Keys.Open)
            {
                open = true;
            }
            return open;
        }
        set
        {
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