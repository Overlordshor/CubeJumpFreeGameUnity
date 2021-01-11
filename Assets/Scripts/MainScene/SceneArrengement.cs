using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneArrengement : MonoBehaviour
{
    public Text GameNameText, PlayGameText, PriceText, ExitText;
    public Buttons Buttons;
    public GameObject MainCube;
    public GameObject ShopListCubes;

    public bool Sound { get; set; }
    public bool Prompt { get; set; }

    private SpawnCubes _spawnCubes;

    [SerializeField]
    private List<CubeData> cubesData;

    public void StartGame(int buildIndex)
    {
        if (SceneManager.GetActiveScene().buildIndex == buildIndex)
        {
            SwtichTextsScene();
            AnimateStartGameUI();
        }
        else
        {
            PlayerPrefs.SetString(Keys.StartImmediately, "true");
            SceneManager.LoadScene(buildIndex);
        }
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

    private void Start()
    {
        _spawnCubes = GetComponent<SpawnCubes>();

        SetLanguage();
        SetSound();
        SetPrompt();
        SetSkin();
        Restart();
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

    private void Restart()
    {
        if (PlayerPrefs.GetString(Keys.StartImmediately) == "true")
        {
            PlayerPrefs.SetString(Keys.StartImmediately, "false");
            StartGame(SceneManager.GetActiveScene().buildIndex);
        }
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
}