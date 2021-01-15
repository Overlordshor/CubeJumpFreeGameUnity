using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneArrengement : MonoBehaviour
{
    private Game _game;
    private SpawnCubes _spawnCubes;

    public Text GameNameText, PlayGameText, PriceText, ExitText;
    public Buttons Buttons;
    public GameObject MainCube;
    public GameObject ShopListCubes;

    [SerializeField]
    private List<CubeData> cubesData;

    public bool Sound { get; set; }
    public bool Prompt { get; set; }

    private void Start()
    {
        _spawnCubes = GetComponent<SpawnCubes>();
        _game = GetComponent<Game>();

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
                    MainCube.GetComponent<MeshRenderer>().material = loadMaterial;
                    break;
                }
            }
        }
    }

    private void SetLanguage()
    {
        if (PlayerPrefs.HasKey(Keys.Language))
        {
            Language.PrintAnyLanguage(PlayGameText,
               "TAP TO PLAY",
               "НАЖМИ ДЛЯ ИГРЫ");
            Language.PrintAnyLanguage(ExitText,
                "Quit the game?",
                "Выйти из игры?");
        }
    }

    private void Update()
    {
        if (!MainCube.GetComponent<Animation>().isPlaying)
        {
            MainCube.AddComponent<Rigidbody>().mass = 10;
            SwitchScriptsScene();
        }
    }

    private void SwtichTextsScene()
    {
        GameNameText.text = "0";
        PlayGameText.gameObject.SetActive(false);
    }

    private void AnimateStartGameUI()
    {
        Buttons.GoAway();
        MainCube.GetComponent<Animation>().Play("StartGameCube");
    }

    private void SwitchScriptsScene()
    {
        _spawnCubes.GetNewCube();

        GetComponentInChildren<JumpClickController>().enabled = true;
        Destroy(this);
    }

    public void StartGame(Mode mode)
    {
        _game.IsMode = mode;
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