using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int _totalScore;
    private int _buffPower;

    [SerializeField]
    private Text _buff, _gameNameText, _record;

    public Transform Cubes;

    private void Start()
    {
        SetRecord(_totalScore);
    }

    private void Update()
    {
        PrintBuffOnScreen();
    }

    private void PrintBuffOnScreen()
    {
        var height = Cubes.childCount;
        _buffPower = height - 1;
        if (_buffPower >= 2)
        {
            Language.PrintAnyLanguage(_buff,
           "Your buff score: " + _buffPower,
           "Уровень твоего бафа:" + _buffPower);
            _buff.gameObject.SetActive(true);

            if (height == 6)
            {
                GoogleServicesManager.UnlockAchievement(Keys.AchievementTowerBabel);
            }
        }
        else if (_buffPower < 2)
        {
            _buff.gameObject.SetActive(false);
        }
    }

    private void SetRecord(int totalScore)
    {
        if (PlayerPrefs.GetInt("Record") < totalScore)
        {
            PlayerPrefs.SetInt("Record", totalScore);
        }
        Language.PrintAnyLanguage(_record,
           "TOP: " + PlayerPrefs.GetInt("Record").ToString(),
           "Рекорд: " + PlayerPrefs.GetInt("Record").ToString());

        if (totalScore >= 100)
        {
            GoogleServicesManager.UnlockAchievement(Keys.AchievementFirstRecord);
        }
    }

    public void Add()
    {
        _totalScore += 1 * _buffPower;
        _gameNameText.text = Convert.ToString(_totalScore);
        SetRecord(_totalScore);
    }
}