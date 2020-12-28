using UnityEngine;

public class AcceptExitButton : MonoBehaviour
{
    public void Exit()
    {
        PlayerPrefs.Save();
        GoogleServicesManager.SingOut();
        Application.Quit();
    }
}