using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowBall : MonoBehaviour
{
    private Image Ball;
    float h;
    float s;
    float v;
    void Start()
    {
        Ball = GetComponent<Image>();
        Color.RGBToHSV(Ball.color, out h, out s, out v);
    }

    void Update()
    {
        Color newColor = Color.HSVToRGB((h + (0.2f * Time.deltaTime)) % 1, s, v);
        Ball.color = newColor;
        Color.RGBToHSV(Ball.color, out h, out s, out v);
    }
}
