using UnityEngine;
using UnityEngine.UI;

public class SceneArrengement : MonoBehaviour
{
    public Text GameNameText, PlayGameText, PriceText, ExitText;
    public Buttons Buttons;
    public GameObject MainCube;
    public GameObject ShopListCubes;

    public bool Sound { get; set; }
    public string KeyMute { get => keyMute; private set => keyMute = value; }

    private SpawnCubes spawnCubes;
    private string keySkin = "Skin";
    private string keyRestart = "Restart";
    private string keyLanguage = "Language";
    private string keyMute = "Mute";

    public void StartGame()
    {
        SwtichTextsScene();

        AnimateStartGameUI();
    }

    private void Start()
    {
        spawnCubes = GetComponent<SpawnCubes>();

        SetLanguage();
        SetSound();
        SetSkin();
        Restart();
    }

    private void SetSound()
    {
        if (PlayerPrefs.HasKey(KeyMute))
        {
            Sound = PlayerPrefs.GetString(KeyMute) != "True";
        }
        else
        {
            Sound = true;
        }

        AudioListener.volume = Sound ? 1f : 0f;
    }

    private void Restart()
    {
        if (PlayerPrefs.GetString(keyRestart) == "true")
        {
            PlayerPrefs.SetString(keyRestart, "false");
            StartGame();
        }
    }

    private void SetSkin()
    {
        if (PlayerPrefs.HasKey(keySkin))
        {
            Material loadMaterial = ShopListCubes.transform.Find(PlayerPrefs.GetString(keySkin)).GetComponent<MeshRenderer>().material;
            MainCube.GetComponent<MeshRenderer>().material = loadMaterial;
        }
    }

    private void SetLanguage()
    {
        if (PlayerPrefs.HasKey(keyLanguage))
        {
            Language.PrintAnyLanguage(PlayGameText,
               "TAP TO PLAY",
               "НАЖМИ ДЛЯ ИГРЫ");
            Language.PrintAnyLanguage(PriceText,
              "200 GOLD",
              "200 ЗОЛОТЫХ");
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
        spawnCubes.GetNewCube();

        GetComponentInChildren<JumpClickController>().enabled = true;
        Destroy(this);
    }
}