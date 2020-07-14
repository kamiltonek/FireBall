using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    private bool haveAds;
    private int bestScore;
    private int starAmount;
    private int selectedSkin;
    private bool allowVibrate;
    List<Skin> skinList;

    public PlayerData()
    {
        this.bestScore = 0;
        this.starAmount = 0;
        this.selectedSkin = 0;
        this.skinList = new List<Skin>();
        this.haveAds = true;
        this.allowVibrate = true;
    }

    public int BestScore { get => bestScore; set => bestScore = value; }
    public int StarAmount { get => starAmount; set => starAmount = value; }
    public int SelectedSkin { get => selectedSkin; set => selectedSkin = value; }
    public List<Skin> SkinList { get => skinList; set => skinList = value; }
    public bool HaveAds { get => haveAds; set => haveAds = value; }
    public bool AllowVibrate { get => allowVibrate; set => allowVibrate = value; }
}
