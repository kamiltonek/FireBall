using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI myScore;
    float h;
    float s;
    float v;
    float alpha;
    void Start()
    {
        myScore = GetComponent<TextMeshProUGUI>();
        Color.RGBToHSV(myScore.color, out h, out s, out v);
        alpha = myScore.color.a;
    }

    void Update()
    {
        myScore.text = GameInfo.instance.score.ToString();
        Color newColor = Color.HSVToRGB((h + (0.05f * Time.deltaTime)) % 1, s, v);
        newColor.a = alpha;
        myScore.color = newColor;
        Color.RGBToHSV(myScore.color, out h, out s, out v);
    }
}
