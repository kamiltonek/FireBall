using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressToStartManager : MonoBehaviour
{
    [SerializeField] private GameObject pressToStartPanel;

    public void startGame()
    {
        pressToStartPanel.SetActive(false);
        Time.timeScale = 1f;
    }

}
