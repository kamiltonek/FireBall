using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBallInMenu : MonoBehaviour
{
    public static SpawnBallInMenu instance;
    [SerializeField] private GameObject[] skinList;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        spawnBall();
    }

    public void spawnBall()
    {
        foreach (Transform ball in transform)
        {
            Destroy(ball.gameObject);
        }
        Instantiate(skinList[SaveAndLoad.getSelectedSkin()], transform.position, Quaternion.identity, transform);
        Time.timeScale = 1f;
    }
}
