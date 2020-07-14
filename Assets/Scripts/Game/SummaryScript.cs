using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummaryScript : MonoBehaviour
{
    [SerializeField] private GameObject summartyBoard;
    [SerializeField] private TextMeshProUGUI myScore;
    [SerializeField] private TextMeshProUGUI bestScore;
    [SerializeField] private TextMeshProUGUI starsCount;
    [SerializeField] private GameObject adButton;

    public static SummaryScript instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void showSummary()
    {
        myScore.text = GameInfo.instance.score.ToString();
        bestScore.text = SaveAndLoad.getBestScore().ToString();
        starsCount.text = GameInfo.instance.stars.ToString();

        if(GameInfo.instance.score > SaveAndLoad.getBestScore())
        {
            SaveAndLoad.setBestScore(GameInfo.instance.score);
        }

        PlayService.PostToLeaderboard(GameInfo.instance.score);

        summartyBoard.SetActive(true);
        GetComponent<Animator>().Play("showSummary");
        StartCoroutine(showAdButton());
    }

    IEnumerator showAdButton()
    {
        while (!AdController.Instance.rewardedVideoIsReady())
        {
            yield return new WaitForSeconds(0.1f);
        }
        if(GameInfo.instance.stars > 0)
        {
            adButton.SetActive(true);
        }
    }
}
