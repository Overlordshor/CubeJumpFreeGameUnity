using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text GameNameText;
    public Text Record;
    public Text Buff;
    public Transform Cubes;

    private int totalScore = 0;

    private int buffPower { get; set; }

    public void Add()
    {
        totalScore += 1 * buffPower;
        GameNameText.text = Convert.ToString(totalScore);
        SetRecord(totalScore);
    }

    private void Start()
    {
        SetRecord(totalScore);
    }

    private void Update()
    {
        PrintBuffOnScreen();
    }

    private void PrintBuffOnScreen()
    {
        var height = Cubes.childCount;
        buffPower = height - 1;
        if (buffPower >= 2)
        {
            Language.PrintAnyLanguage(Buff,
           "Your buff score: " + buffPower,
           "Уровень твоего бафа:" + buffPower);
            Buff.gameObject.SetActive(true);
        }
        else if (buffPower < 2)
        {
            Buff.gameObject.SetActive(false);
        }
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