using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI starsAmount;

    void Start()
    {
        setStarsAmount();
    }

    void Update()
    {
        setStarsAmount();
    }

    private void setStarsAmount()
    {
        starsAmount.text = SaveAndLoad.getPlayerStars().ToString();
    }
}
