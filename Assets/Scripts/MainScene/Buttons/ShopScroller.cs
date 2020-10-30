using UnityEngine;

public class ShopScroller : MonoBehaviour
{
    public GameObject Cubes;
    public GameObject MainCube;

    private Vector3 oldMousePosition, newMousePosition;
    private float lockedYPosition;
    private float lockedZPosition;
    private readonly float distanceToCube = 3.5f;

    private string[] cubesName;
    private string keyOpen = "Open";
    private int selectNumberCube = 0;

    private ButtonAccept buttonAccept;

    private AudioSource audioSource;

    public string GetNameCube()
    {
        return cubesName[selectNumberCube];
    }

    public void GetMaterialCube()
    {
        MainCube.GetComponent<MeshRenderer>().material =
            Cubes.transform.GetChild(selectNumberCube).GetComponent<MeshRenderer>().material;
        PlayerPrefs.SetInt("Skin", selectNumberCube);
    }

    private string[] GetListCubesName()
    {
        string[] cubesName = new string[Cubes.transform.childCount];
        for (int i = 0; i < Cubes.transform.childCount; i++)
        {
            cubesName[i] = Cubes.transform.GetChild(i).name;
        }
        return cubesName;
    }

    private void Start()
    {
        lockedYPosition = Cubes.transform.position.y;
        lockedZPosition = Cubes.transform.position.z;
        cubesName = GetListCubesName();
        PlayerPrefs.SetString(cubesName[0], keyOpen);

        buttonAccept = FindObjectOfType<ButtonAccept>();
        audioSource = GetComponent<AudioSource>();
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
        if (newMousePosition.x > oldMousePosition.x && selectNumberCube > 0) // move mouse right;
        {
            Cubes.transform.position = new Vector3(Cubes.transform.position.x + distanceToCube, lockedYPosition, lockedZPosition);
            selectNumberCube--;
        }
        else if (newMousePosition.x < oldMousePosition.x && selectNumberCube < Cubes.transform.childCount - 1) // move mouse left;
        {
            Cubes.transform.position = new Vector3(Cubes.transform.position.x - distanceToCube, lockedYPosition, lockedZPosition);
            selectNumberCube++;
        }
        audioSource.Play();
        buttonAccept.GetImage();
    }
}