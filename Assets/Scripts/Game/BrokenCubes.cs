using UnityEngine;

public class BrokenCubes : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1.5f);
    }
}