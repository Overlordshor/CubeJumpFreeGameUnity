using UnityEngine;

public class ShopScroller : MonoBehaviour
{
    public GameObject Cubes;

    private Vector3 oldMousePosition, newMousePosition;
    private float lockedYPosition;
    private float lockedZPosition;
    private readonly float distanceToCube = 3.8f;

    private int selectNumberCube = 1;

    private void Start()
    {
        lockedYPosition = Cubes.transform.position.y;
        lockedZPosition = Cubes.transform.position.z;
    }

    private void OnMouseDown()
    {
        oldMousePosition = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        newMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
    }

    private void OnMouseUp()
    {
        if (newMousePosition.x > oldMousePosition.x && selectNumberCube > 1) // move right;
        {
            Cubes.transform.position = new Vector3(Cubes.transform.position.x + distanceToCube, lockedYPosition, lockedZPosition);
            selectNumberCube--;
        }
        else if (newMousePosition.x < oldMousePosition.x && selectNumberCube < Cubes.transform.childCount) // move left;
        {
            Cubes.transform.position = new Vector3(Cubes.transform.position.x - distanceToCube, lockedYPosition, lockedZPosition);
            selectNumberCube++;
        }
    }
}