using UnityEngine;

public class SpawnCubes : MonoBehaviour
{
    public GameObject Cube;
    public GameObject MainCube;
    public GameObject CubesParent;

    private int countCube;
    private Score score;
    private JumpClickController jumpClickController;

    public void GetNewCube()
    {
        var cube = Instantiate(Cube, new Vector3(-4f, -4.08f, 5f), Quaternion.Euler(new Vector3(0, 60, 0)));
        cube.transform.parent = CubesParent.transform;
        cube.layer = 9;
        jumpClickController.GetControl(cube);

        countCube++;
        score.Add(countCube);
    }

    private void Start()
    {
        score = gameObject.GetComponent<Score>();
        jumpClickController = gameObject.GetComponentInChildren<JumpClickController>();
    }
}