using UnityEngine;
using UnityEngine.UI;

public class ClickDetector : MonoBehaviour
{
    public Text GameNameText, PlayGameText;
    public Buttons buttons;
    public GameObject MainCube;

    private void OnMouseDown()
    {
        GameNameText.text = "0";
        PlayGameText.gameObject.SetActive(false);
        buttons.GoAway();
        MainCube.GetComponent<Animation>().Play("StartGameCube");
    }
}