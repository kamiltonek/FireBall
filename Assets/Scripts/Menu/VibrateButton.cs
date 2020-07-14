using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrateButton : MonoBehaviour
{
    [SerializeField] private Sprite vibrateON;
    [SerializeField] private Sprite vibrateOFF;
    void Start()
    {
        refreshVibrateButton();
    }

    private void refreshVibrateButton()
    {
        if (SaveAndLoad.allowVibrate())
        {
            GetComponent<Image>().sprite = vibrateON;
        }
        else
        {
            GetComponent<Image>().sprite = vibrateOFF;
        }
    }

    public void OnOffVibrate()
    {
        SaveAndLoad.changeAllowVibrate();
        refreshVibrateButton();
    }
}
