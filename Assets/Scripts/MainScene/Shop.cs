using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject ClickDetector;

    private void Start()
    {
        ClickDetector.SetActive(false);
    }

    private void OnDisable()
    {
        ClickDetector.SetActive(true);
    }
}