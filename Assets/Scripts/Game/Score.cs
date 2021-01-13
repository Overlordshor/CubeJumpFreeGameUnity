using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int totalScore;
    private int _buffPower;
    [SerializeField] private GameObject[] _fires;

    [SerializeField]
    private Text _buff, _gameNameText, _record;

    public Transform Cubes;

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
        _buffPower = height - 1;
        if (_buffPower >= 2)
        {
            Language.PrintAnyLanguage(_buff,
           "Your buff score: " + _buffPower,
           "Уровень твоего бафа:" + _buffPower);
            _buff.gameObject.SetActive(true);
            if (_buffPower >= 15)
            {
                ActivateFire(2); // green;
            }
            else if (_buffPower >= 7)
            {
                ActivateFire(1); // red;
            }
            else if (_buffPower < 7)
            {
                ActivateFire(0); // yellow;
            }

            if (height == 6)
            {
                GoogleServicesManager.UnlockAchievement(Keys.AchievementTowerBabel);
            }
        }
        else if (_buffPower < 2)
        {
            _buff.gameObject.SetActive(false);
            DeactivateFire();
        }
    }

    private void DeactivateFire()
    {
        foreach (var fire in _fires)
        {
            fire.SetActive(false);
        }
    }

    private void ActivateFire(int indexFire)
    {
        DeactivateFire();
        _fires[2].SetActive(true);
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
        totalScore += 1 * _buffPower;
        _gameNameText.text = Convert.ToString(totalScore);
        SetRecord(totalScore);
    }
}