using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text GameNameText;
    public Text Record;
    public Text Buff;

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

    public void PrintBuffOnScreen()
    {
        if (buffPower <= 1)
        {
            Buff.gameObject.SetActive(false);
        }
        else
        {
            Buff.text = "Your buff score: " + buffPower;
            Buff.gameObject.SetActive(true);
        }
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