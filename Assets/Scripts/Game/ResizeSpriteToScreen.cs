using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeSpriteToScreen : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 0f;
        ResizeSprite();
    }

    private void ResizeSprite()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        double worldScreenHeight = Camera.main.orthographicSize * 2.0;
        double worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 newSize = new Vector3((float)(worldScreenWidth / width), (float)(worldScreenHeight / height), 1);


        transform.localScale = newSize;
    }
}
