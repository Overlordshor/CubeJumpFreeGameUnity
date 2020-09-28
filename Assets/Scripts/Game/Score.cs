using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text GameNameText;
    public Text Record;

    private int totalScore = 0;
    private int buffPower;

    public void Add()
    {
        totalScore += 1 * buffPower;
        GameNameText.text = Convert.ToString(totalScore);
        SetRecord(totalScore);
    }

    public void RefreshBuff(int heightTower)
    {
        buffPower = heightTower - 2;
    }

    private void Start()
    {
        SetRecord(totalScore);
    }

    private void SetRecord(int totalScore)
    {
        if (PlayerPrefs.GetInt("Record") < totalScore)
        {
            PlayerPrefs.SetInt("Record", totalScore);
        }
        Record.text = "TOP: " + PlayerPrefs.GetInt("Record").ToString();
    }
}