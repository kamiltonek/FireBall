using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeToEqualXY : MonoBehaviour
{
    void Start()
    {
        float scale = GameObject.Find("Game").transform.localScale.x;
        transform.localScale = new Vector3(scale, scale, 1);
    }
}
