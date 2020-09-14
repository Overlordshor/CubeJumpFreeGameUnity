using UnityEngine;

public class Buttons : MonoBehaviour
{
    private Vector3 originalScale;
    private Vector3 pressedScale;
    private float speed;
    private bool move;
    private RectTransform button;

    public void GoAway()
    {
        speed = 5f;
        move = true;
        Destroy(gameObject, 1f);
    }

    private void Start()
    {
        originalScale = new Vector3(0.01915709f, 0.01915709f);
        pressedScale = new Vector3(0.027f, 0.027f);
        button = GetComponent<RectTransform>();
        move = false;
    }

    private void Update()
    {
        if (move)
        {
            button.offsetMin -= new Vector2(0, speed);
        }
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