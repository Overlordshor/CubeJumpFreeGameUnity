using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject ClickDetector;

    private void OnEnable()
    {
        ClickDetector.SetActive(false);
    }

    private void OnDisable()
    {
        ClickDetector.SetActive(true);
    }
}