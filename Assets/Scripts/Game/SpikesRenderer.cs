using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesRenderer : MonoBehaviour
{
    public static SpikesRenderer instance;

    [SerializeField] private GameObject topSpikesSpawner;
    [SerializeField] private GameObject downSpikesSpawner;
    [SerializeField] private GameObject spikePrefab;
    private float distance;
    private const int spikesCount = 8;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        renderSpikeDown();
    }

    public void renderSpikeTop()
    {
        renderSpikes(topSpikesSpawner);
    }

    public void renderSpikeDown()
    {
        renderSpikes(downSpikesSpawner);
    }

    private void renderSpikes(GameObject spawner)
    {
        List<int> positions = randomValues(GameInfo.instance.getSpikesAmount());

        for(int i = 0; i < spikesCount; i++)
        {
            if(!positions.Contains(i))
            {
                spawner.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                spawner.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    private List<int> randomValues(int amount)
    {
        List<int> values = new List<int>();
        List<int> returnedValues = new List<int>();

        for (int i = 0; i < spikesCount; i++)
        {
            values.Add(i);
        }

        for(int i = 0; i < amount; i++)
        {
            int random = UnityEngine.Random.Range(0, values.Count);
            returnedValues.Add(values[random]);
            values.RemoveAt(random);
        }

        return returnedValues;
    }
}
