using UnityEngine;

public class CancelExitButton : MonoBehaviour
{
    public GameObject Buttons, ClickDetector;

    private void OnDisable()
    {
        if (ClickDetector.GetComponent<JumpClickController>().enabled == false)
        {
            Buttons.SetActive(true);
        }
    }
}