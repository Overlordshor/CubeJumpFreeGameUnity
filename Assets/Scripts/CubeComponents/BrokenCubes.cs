using UnityEngine;

public class BrokenCubes : MonoBehaviour
{
    public void PassMaterial(Material materialCube)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponentInChildren<MeshRenderer>().material = materialCube;
        }
    }

    private void Start()
    {
        Destroy(gameObject, 1.5f);
    }
}