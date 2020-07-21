using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private GameObject holder;
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject myScoreField;
 
    [SerializeField] private GameObject labelPrefabDefault;
    [SerializeField] private GameObject labelPrefabGold;
    [SerializeField] private GameObject labelPrefabSilver;
    [SerializeField] private GameObject labelPrefabBronze;

    [SerializeField] private GameObject loadingPanel;
    private bool load;
    private bool isExecuting;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && holder.activeSelf)
        {
            closeLeaderboard();
        }
    }
    private void loadLeaderboard()
    {
        if (isExecuting)
            return;
        isExecuting = true;
        clearLeaderboard();

        PlayGamesPlatform.Instance.LoadScores(
        GPGSIds.leaderboard_highscore,
        LeaderboardStart.TopScores,
        20, // row count
        LeaderboardCollection.Public,
        LeaderboardTimeSpan.AllTime,
        (LeaderboardScoreData data) => {
            List<string> userIDs = new List<string>();

            Dictionary<string, IScore> userScores = new Dictionary<string, IScore>();
            for (int i = 0; i < data.Scores.Length; i++)
            {
                IScore score = data.Scores[i];
                userIDs.Add(score.userID);
                userScores[score.userID] = score;
            }

            Dictionary<string, string> userNames = new Dictionary<string, string>();
            Social.LoadUsers(userIDs.ToArray(), (users) => {
                for (int i = 0; i < users.Length; i++)
                {
                    userNames[users[i].id] = users[i].userName;
                }

                for (int i = 0; i < data.Scores.Length; i++)
                {                 
                    IScore score = data.Scores[i];
                    string userName = userNames[score.userID];
                    int rank = score.rank;

                    fillLeadboard(rank, userName, score.value);
                    
                }
                try
                {
                    IScore myScore = data.PlayerScore;
                    string myName = userNames[myScore.userID];
                    int myRank = myScore.rank;
                    myScoreField.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "#" + myRank.ToString() + ".";
                    myScoreField.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = myName;
                    myScoreField.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = myScore.value.ToString();
                }
                catch(Exception e)
                {
                    Debug.LogError("issue");
                    myScoreField.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "# ---";
                    myScoreField.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = " --- ";
                    myScoreField.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "0";
                }
                
                //if(data.NextPageToken != null)
                //{
                //    LoadMoreScores(data.NextPageToken);
                //}
                isExecuting = false;
                load = true;
            });
        });

    }

    private void LoadMoreScores(ScorePageToken token)
    {
        PlayGamesPlatform.Instance.LoadMoreScores(
            token,
            20,
            (LeaderboardScoreData data) => {
                List<string> userIDs = new List<string>();

                Dictionary<string, IScore> userScores = new Dictionary<string, IScore>();
                for (int i = 0; i < data.Scores.Length; i++)
                {
                    IScore score = data.Scores[i];
                    userIDs.Add(score.userID);
                    userScores[score.userID] = score;
                }

                Dictionary<string, string> userNames = new Dictionary<string, string>();
                Social.LoadUsers(userIDs.ToArray(), (users) => {
                    for (int i = 0; i < users.Length; i++)
                    {
                        userNames[users[i].id] = users[i].userName;
                    }

                    for (int i = 0; i < data.Scores.Length; i++)
                    {
                        IScore score = data.Scores[i];
                        string userName = userNames[score.userID];
                        int rank = score.rank;

                        fillLeadboard(rank, userName, score.value);

                    }                  

                    if (data.NextPageToken != null && container.transform.childCount < 100)
                    {
                        LoadMoreScores(data.NextPageToken);
                    }

                });
            });

    }
    private void clearLeaderboard()
    {
        foreach(Transform obj in container.transform)
        {
            Destroy(obj.gameObject);
        }
    }

    private void fillLeadboard(int lp, string name, long score)
    {
        GameObject newLabel = null;
        switch (lp)
        {
            case 1:
                newLabel = labelPrefabGold;
                break;
            case 2:
                newLabel = labelPrefabSilver;
                break;
            case 3:
                newLabel = labelPrefabBronze;
                break;
            default:
                newLabel = labelPrefabDefault;
                break;
        }
        newLabel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "#" + lp.ToString() + ".";
        newLabel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;
        newLabel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = score.ToString();

        Instantiate(newLabel, container.transform.position, Quaternion.identity, container.transform);
    }

    public void showLeaderboard()
    {
        if (!PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayService.SignInToGooglePlayService();
        }
        else
        {
            StartCoroutine(LoadAsynchronously());
        }
    }

    IEnumerator LoadAsynchronously()
    {
        load = false;
        float time = 0f;
        loadingPanel.SetActive(true);
        loadLeaderboard();
        while (!load)
        {
            time += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Escape) || time > 10f)
            {
                loadingPanel.SetActive(false);
                yield break;
            }
            yield return null;
        }
        loadingPanel.SetActive(false);
        holder.SetActive(true);   
    }

    public void closeLeaderboard()
    {
        holder.SetActive(false);
    }
}
