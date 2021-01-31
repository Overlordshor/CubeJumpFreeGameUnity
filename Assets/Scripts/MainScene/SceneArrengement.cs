using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class SceneArrengement : MonoBehaviour
{
    private SpawnCubes _spawnCubes;

    [SerializeField] private GameObject _cubePrefab, _mainCube;

    [SerializeField] private Text _gameNameText, _playGameText, _exitText, _livesText;
    [SerializeField] private Buttons _buttons;

    [SerializeField]
    private List<CubeData> cubesData;

    public bool Sound { get; set; }
    public bool Prompt { get; set; }

    private void Start()
    {
        _spawnCubes = GetComponent<SpawnCubes>();

        SetLanguage();
        SetSound();
        SetPrompt();
        SetSkin();

        if (PlayerPrefs.GetString(Keys.StartImmediately) == "true")
        {
            PlayerPrefs.DeleteKey(Keys.StartImmediately);
            var mode = (Mode)PlayerPrefs.GetInt(Keys.Mode);
            StartGame(mode);
        }
    }

    private void SetSound()
    {
        if (PlayerPrefs.HasKey(Keys.Mute))
        {
            Sound = PlayerPrefs.GetString(Keys.Mute) != "True";
        }
        else
        {
            Sound = true;
        }

        AudioListener.volume = Sound ? 1f : 0f;
    }

    private void SetSkin()
    {
        if (PlayerPrefs.HasKey(Keys.Skin))
        {
            foreach (var cube in cubesData)
            {
                if (PlayerPrefs.GetString(Keys.Skin) == cube.ID)
                {
                    Material loadMaterial = cube.Material;
                    _mainCube.GetComponent<MeshRenderer>().material = loadMaterial;
                    break;
                }
            }
        }
    }

    private void SetLanguage()
    {
        if (!PlayerPrefs.HasKey(Keys.Language))
        {
            var currentCultureName = CultureInfo.CurrentCulture.Name;
            if (currentCultureName == "ru-RU")
            {
                PlayerPrefs.SetString(Keys.Language, "Russian");
            }
        }
        if (PlayerPrefs.HasKey(Keys.Language))
        {
            Language.PrintAnyLanguage(_playGameText,
               "TAP TO PLAY",
               "НАЖМИ ДЛЯ ИГРЫ");
            Language.PrintAnyLanguage(_exitText,
                "Quit the game?",
                "Выйти из игры?");
            var countLives = _livesText.GetComponent<Lives>();
            countLives.Print();
        }
    }

    private void Update()
    {
        if (!_mainCube.GetComponent<Animation>().isPlaying)
        {
            _mainCube.AddComponent<Rigidbody>().mass = 10;
            SwitchScriptsScene();
        }
    }

    private void SwtichTextsScene()
    {
        _gameNameText.text = "0";
        _playGameText.gameObject.SetActive(false);
    }

    private void AnimateStartGameUI()
    {
        _buttons.GoAway();
        _mainCube.GetComponent<Animation>().Play("StartGameCube");
    }

    private void SwitchScriptsScene()
    {
        GetComponentInChildren<JumpClickController>().enabled = true;

        _spawnCubes.GetCube(_cubePrefab, _mainCube);

        enabled = false;
    }

    public void StartGame(Mode mode)
    {
        Game.IsMode = mode;
        SwtichTextsScene();
        AnimateStartGameUI();
    }

    public void SetPrompt()
    {
        if (PlayerPrefs.HasKey(Keys.Prompt))
        {
            Prompt = PlayerPrefs.GetString(Keys.Prompt) == "True";
        }
        else
        {
            Prompt = true;
            PlayerPrefs.SetString(Keys.Prompt, "True");
        }
    }
}