using UnityEngine;

public class Star : MonoBehaviour
{
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Mathf.PingPong(Time.time * 2, 1.0f));
    }
}