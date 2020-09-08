using UnityEngine;

public class Buttons : MonoBehaviour
{
    private Vector3 originalScale;
    private Vector3 pressedScale;

    private void Start()
    {
        originalScale = new Vector3(0.01915709f, 0.01915709f);
        pressedScale = new Vector3(0.027f, 0.027f);
    }

    private void OnMouseDown()
    {
        transform.localScale = pressedScale;
    }

    private void OnMouseUp()
    {
        transform.localScale = originalScale;
    }
}