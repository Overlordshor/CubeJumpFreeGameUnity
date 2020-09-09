using UnityEngine.UI;
using UnityEngine;

public class TextFade : MonoBehaviour
{
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.PingPong(Time.time, 1f));
    }
}