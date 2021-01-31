using UnityEngine;
using UnityEngine.UI;

public class PanelFade : MonoBehaviour
{
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, Mathf.PingPong(Time.time, 1f));
    }
}