using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Skin
{
    private int index;
    private bool owned;
    private int cost;

    public Skin(int index, bool owned, int cost)
    {
        this.index = index;
        this.owned = owned;
        this.cost = cost;
    }

    public int Index { get => index; set => index = value; }
    public bool Owned { get => owned; set => owned = value; }
    public int Cost { get => cost; set => cost = value; }
}
