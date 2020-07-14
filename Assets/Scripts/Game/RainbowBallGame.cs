using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowBallGame : MonoBehaviour
{
    private SpriteRenderer Ball;
    float h;
    float s;
    float v;
    void Start()
    {
        Ball = GetComponent<SpriteRenderer>();
        Color.RGBToHSV(Ball.color, out h, out s, out v);
    }

    void Update()
    {
        Color newColor = Color.HSVToRGB((h + (0.2f * Time.deltaTime)) % 1, s, v);
        Ball.color = newColor;
        Color.RGBToHSV(Ball.color, out h, out s, out v);
    }
}
