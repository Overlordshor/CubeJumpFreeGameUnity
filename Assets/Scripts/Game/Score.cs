using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text GameNameText;

    private int totalScore = 0;
    private int buffPower;

    public void Add()
    {
        totalScore += 1 * buffPower;
        GameNameText.text = Convert.ToString(totalScore);
    }

    public void RefreshBuff(int heightTower)
    {
        buffPower = heightTower - 2;
    }
}