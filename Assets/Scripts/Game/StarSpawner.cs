using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public static StarSpawner instance;
    [SerializeField] private GameObject starPref;
    [SerializeField] private GameObject starPremiumPref;
    private float distance;
    private float lifeTime = 3f;
    private int chanceToSpawnStar = 30;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        float leftBound = GameObject.Find("left").GetComponent<BoxCollider2D>().offset.x - 0.5f * GameObject.Find("left").GetComponent<BoxCollider2D>().size.x;
        float rightBound = GameObject.Find("right").GetComponent<BoxCollider2D>().offset.x - 0.5f * GameObject.Find("left").GetComponent<BoxCollider2D>().size.x;
        float scale = GameObject.Find("Game").transform.localScale.x;
        
        distance = (rightBound * scale - leftBound * scale) * 0.75f;
    }

    public void tryShowStar()
    {
        if(GameInfo.instance.score == 100)
        {
            spawnStar(starPremiumPref);
        }
        int random = Random.Range(1, 101);
        if(random < chanceToSpawnStar)
        {
            spawnStar(starPref);
        }
    }
    private void spawnStar(GameObject pref)
    {
        Vector2 position;
        position.x = Random.Range(-distance / 2, distance / 2);
        position.y = Random.Range(-distance / 2, distance / 2);
        GameObject star = Instantiate(pref, position, Quaternion.identity, transform);
        if (pref.name == "star")
        {
            StartCoroutine(StartAnimation(star));
        }
    }

    IEnumerator StartAnimation(GameObject star)
    {
        yield return new WaitForSeconds(lifeTime);

        if(star != null)
        {
            star.GetComponent<Animator>().Play("destroyStar");
        }

    }
}
