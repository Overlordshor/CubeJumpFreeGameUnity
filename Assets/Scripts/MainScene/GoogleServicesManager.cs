using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GoogleServicesManager : MonoBehaviour
{
    public static bool IsAuthenticate;

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
        PlayGamesClientConfiguration configuration = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(configuration);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(succes =>
        {
            IsAuthenticate = succes;
            Debug.Log("Social " + succes);
        });
    }
}