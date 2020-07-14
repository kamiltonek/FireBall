using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class PlayService : MonoBehaviour
{
    public static void SignInToGooglePlayService()
    {
        PlayGamesClientConfiguration.Builder builder = new PlayGamesClientConfiguration.Builder();
        PlayGamesPlatform.InitializeInstance(builder.Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate((bool success, string err) => {
            if (success)
            {
                Debug.Log("Login success");
            }
            else
            {
                Debug.Log("Login failed");
                Debug.Log("Error: " + err);
            }
            SceneManager.LoadScene("Menu");
        });
    }

    public static void PostToLeaderboard(int newScore)
    {
        Social.ReportScore(newScore, GPGSIds.leaderboard_highscore, (bool success) =>
        {
            if (success)
            {
                Debug.Log("Posted new score to leaderboard");
            }
            else
            {
                Debug.Log("Unable to post new score to leaderboard");
            }
        });
    }

    public static void ShowLeaderboardUI()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_highscore);
    }

}
