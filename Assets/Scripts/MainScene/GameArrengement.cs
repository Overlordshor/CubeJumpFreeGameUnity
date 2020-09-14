using UnityEngine;
using UnityEngine.UI;

public class GameArrengement : MonoBehaviour
{
    public Text GameNameText, PlayGameText;
    public Buttons Buttons;
    public GameObject MainCube;

    private void OnMouseDown()
    {
        SwtichTextsScene();

        AnimateStartGameUI();

        SwitchScriptsScene();
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
        GetComponent<CubeJump>().enabled = true;
        Destroy(this);
    }
}