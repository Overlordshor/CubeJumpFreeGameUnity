using UnityEngine;
using UnityEngine.SceneManagement;

public class MakeScreenshotByMouseClick : MonoBehaviour
{
#if UNITY_EDITOR
    public Camera MainCamera;

    private int counter = 1;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ScreenCapture.CaptureScreenshot("Assets/Screenshots/Sreenshot" + counter.ToString("00") + "_" + MainCamera.pixelWidth + "x" + MainCamera.pixelHeight + "_" + "_SceneID" + SceneManager.GetActiveScene().name + ".png");
            counter++;
        }
    }

#endif
}