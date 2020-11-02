using UnityEngine;
using UnityEngine.UI;

public class GameArrengement : MonoBehaviour
{
    public Text GameNameText, PlayGameText, PriceText;
    public Buttons Buttons;
    public GameObject MainCube;
    public GameObject ShopListCubes;

    private SpawnCubes spawnCubes;
    private string keySkin = "Skin";

    private void Start()
    {
        spawnCubes = GetComponentInParent<SpawnCubes>();

        Language.PrintAnyLanguage(PlayGameText,
           "TAP TO PLAY",
           "НАЖМИ ДЛЯ ИГРЫ");
        Language.PrintAnyLanguage(PriceText,
          "200 GOLD",
          "200 ЗОЛОТЫХ");
        if (PlayerPrefs.HasKey(keySkin))
        {
            Material loadMaterial = ShopListCubes.transform.GetChild(PlayerPrefs.GetInt(keySkin)).GetComponent<MeshRenderer>().material;
            MainCube.GetComponent<MeshRenderer>().material = loadMaterial;
        }
    }

    private void Update()
    {
        if (!MainCube.GetComponent<Animation>().isPlaying)
        {
            MainCube.AddComponent<Rigidbody>();
            SwitchScriptsScene();
        }
        //#if UNITY_IOS || UNITY_ANDROID
        //        if (Input.touchCount > 0)
        //        {
        //            Touch touch = Input.GetTouch(0);
        //            if (touch.phase == TouchPhase.Began)
        //            {
        //                SwtichTextsScene();

        //                AnimateStartGameUI();
        //            }
        //        }
        //#endif
    }

#if UNITY_STANDALONE || UNITY_IOS || UNITY_ANDROID

    private void OnMouseDown()
    {
        SwtichTextsScene();

        AnimateStartGameUI();
    }

#endif

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
        GetComponent<JumpClickController>().enabled = true;
        Destroy(this);
    }
}