using UnityEngine;
using UnityEngine.UI;

public class GameArrengement : MonoBehaviour
{
    public Text GameNameText, PlayGameText;
    public Buttons Buttons;
    public GameObject MainCube;
    public GameObject Scene;

    private void OnMouseDown()
    {
        GameNameText.text = "0";
        PlayGameText.gameObject.SetActive(false);
        Buttons.GoAway();
        MainCube.GetComponent<Animation>().Play("StartGameCube");
        Scene.GetComponent<SpawnCubes>().enabled = true;
        Destroy(this);
    }
}