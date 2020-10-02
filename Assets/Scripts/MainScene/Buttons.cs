using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public GameObject ButtonVolume, ButtonPrompt;

    //private Vector3 originalScale;
    //private Vector3 pressedScale;

    private float speed;
    private bool move = false;
    private RectTransform button;

    public void GoAway()
    {
        speed = 5f;
        move = true;
        Destroy(gameObject, 1f);
    }

    private void Start()
    {
        //originalScale = new Vector3(0.01915709f, 0.01915709f);
        //pressedScale = new Vector3(0.027f, 0.027f);

        button = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (move)
        {
            button.offsetMin -= new Vector2(0, speed);
        }
    }

    //private void OnMouseDown()
    //{
    //    SetLocalScale(pressedScale);
    //}

    //private void OnMouseUp()
    //{
    //    SetLocalScale(originalScale);
    //}

    protected void OnMouseUpAsButton()
    {
        switch (gameObject.name)
        {
            case "SettingButton":
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(!transform.GetChild(i).gameObject.activeSelf);
                }
                break;

            case "Volume":
                ButtonVolume.GetComponent<ButtonVolume>().GetVolume();
                break;

            case "Prompt":
                ButtonPrompt.GetComponent<ButtonPrompt>().GetPrompt();
                break;
        }
    }

    private void SetLocalScale(Vector3 scale)
    {
        transform.localScale = scale;
    }
}