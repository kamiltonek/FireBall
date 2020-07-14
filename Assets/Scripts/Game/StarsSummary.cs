using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarsSummary : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI starsAmountField;

    void Start()
    {
        starsAmountField.text = SaveAndLoad.getPlayerStars().ToString();
        StartCoroutine(addStars());
    }

    IEnumerator addStars()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            starsAmountField.text = SaveAndLoad.getPlayerStars().ToString();
        }
    }
}
