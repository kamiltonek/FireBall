using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    public static GameInfo instance;
    public int score { get; private set; }
    public int stars { get; private set; }
    public bool end { get; private set; }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        score = 0;
    }

    public void increaseScore()
    {
        score++;
        if(score == 20)
        {
            platform.SetActive(true);
        }
    }
    public void increaseStars()
    {
        stars++;
        SaveAndLoad.increasePlayerStars(1);
    }

    public void increaseStars(int value)
    {
        stars++;
        SaveAndLoad.increasePlayerStars(value);
    }

    public void endGame()
    {
        if (end)
        {
            return;
        }
        
        end = true;
        SummaryScript.instance.showSummary();
    }

    public int getSpikesAmount()
    {
        if (score < 5)
            //return UnityEngine.Random.Range(4, 7); //2 3
            return UnityEngine.Random.Range(2, 5); //2 3
        else if (score < 15)
            return UnityEngine.Random.Range(5, 8); //3 4
        else
            //return UnityEngine.Random.Range(6, 8); //3 4 5
            return UnityEngine.Random.Range(2, 5);

    }
}
