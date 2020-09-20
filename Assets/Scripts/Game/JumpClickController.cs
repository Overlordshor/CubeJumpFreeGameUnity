using UnityEngine;
using UnityEngine.UI;

public class JumpClickController : MonoBehaviour
{
    public GameObject Cube;
    public Text RulesText;

    private bool clickDetected;
    private float startTime;
    private CubeJump gameCube;

    public void GetControl(GameObject cube)
    {
        gameCube = cube.GetComponentInChildren<CubeJump>();
    }

    private void Start()
    {
        gameCube = Cube.GetComponentInChildren<CubeJump>();
        clickDetected = false;
        RulesText.text = "Press and hold to jump. Get points for every cube hit";
        RulesText.gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        gameCube.Squeeze(clickDetected);
    }

    private void OnMouseDown()
    {
        clickDetected = true;
        startTime = Time.time;
    }

    private void OnMouseUp()
    {
        clickDetected = false;

        var pushTime = Time.time - startTime;
        gameCube?.Jump(pushTime);

        if (RulesText.gameObject.activeSelf)
        {
            RulesText.gameObject.SetActive(false);
        }
    }
}