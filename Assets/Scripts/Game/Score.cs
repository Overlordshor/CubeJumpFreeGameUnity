using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int _totalScore;
    private int _buffPower;
    private Text _gameNameText;

    [SerializeField]
    private Text _buff, _record;

    [SerializeField]
    private Transform _cubes;

    private void Start()
    {
        _gameNameText = GetComponent<Text>();
        SetRecord(_totalScore);
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

    public void PrintBuffOnScreen()
    {
        var height = _cubes.childCount;
        _buffPower = height;
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

    public void Add()
    {
        if (_buffPower == 0)
        {
            _totalScore += 1;
        }
        else
        {
            _totalScore += 1 * _buffPower;
        }

        _gameNameText.text = Convert.ToString(_totalScore);
        SetRecord(_totalScore);
    }
}