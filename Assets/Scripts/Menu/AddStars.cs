using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStars : MonoBehaviour
{
    public void addStars(int value)
    {
        SaveAndLoad.increasePlayerStars(value);
    }
}
