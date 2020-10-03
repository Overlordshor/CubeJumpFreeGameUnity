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
            Language.PrintAnyLanguage(Buff,
           "Your buff score: " + buffPower,
           "Уровень твоего бафа:" + buffPower);
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
        Language.PrintAnyLanguage(Record,
           "TOP: " + PlayerPrefs.GetInt("Record").ToString(),
           "Рекорд: " + PlayerPrefs.GetInt("Record").ToString());
    }
}