using UnityEngine;
using UnityEngine.UI;

public class GameArrengement : MonoBehaviour
{
    public Text GameNameText, PlayGameText;
    public Buttons buttons;
    public GameObject MainCube;
    public GameObject Scene;

    private void OnMouseDown()
    {
        GameNameText.text = "0";
        PlayGameText.gameObject.SetActive(false);
        buttons.GoAway();
        MainCube.GetComponent<Animation>().Play("StartGameCube");
        Scene.GetComponent<SpawnCubes>().enabled = true;
    }
}