using System.Collections;
using UnityEngine;

public class CoroutineSpawnStars : MonoBehaviour
{
    public GameObject Star;

    private void Start()
    {
        StartCoroutine("spawn");
    }

    private IEnumerator spawn()
    {
        while (true)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(
                new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height),
                Camera.main.farClipPlane / 2));

            Instantiate(Star, position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
}