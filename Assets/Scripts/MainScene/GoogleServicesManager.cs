using UnityEngine;
using GooglePlayGames;

public class GoogleServicesManager : MonoBehaviour
{
    public static void ReportScore(int totalScore)
    {
        Social.ReportScore(totalScore, Keys.LeaderBoard, (bool success) => { });
    }

    public static void ShowLeaderboardUI()
    {
        Social.ShowLeaderboardUI();
    }

    public static void SingOut()
    {
        PlayGamesPlatform.Instance.SignOut();
    }

    private void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate(succes =>
        {
            Debug.Log("Social " + succes);
        });
    }
}