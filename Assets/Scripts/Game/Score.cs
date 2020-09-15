using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text GameNameText;

    private int score;

    public void Add(int countCube)
    {
        score = countCube - 1; // -firstCube;
        GameNameText.text = Convert.ToString(score);
    }
}