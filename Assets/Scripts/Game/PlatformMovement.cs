using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public enum Direction { Left, Right}

    private Rigidbody2D myBody;
    private Direction myDireciton;
    private float moveSpeed = 10f;
    private float leftBound = -2.14f;
    private float rightBound = 2.14f;
    private float distance;
    void Start()
    {
        int randomValue = UnityEngine.Random.Range(0, 2);
        //myDireciton = randomValue == 0 ? Direction.Left : Direction.Right;
        myDireciton = Direction.Right;
        myBody = GetComponent<Rigidbody2D>();

        float leftBound = GameObject.Find("left").GetComponent<BoxCollider2D>().offset.x;
        float rightBound = GameObject.Find("right").GetComponent<BoxCollider2D>().offset.x;
        float scale = GameObject.Find("Game").transform.localScale.x;

        distance = rightBound * scale - leftBound * scale;
    }

    void FixedUpdate()
    {
        if (myDireciton == Direction.Left)
        {
            move(-1);
        }
        else
        {
            move(1);
        }
    }

    private void move(int direction)
    {
        myBody.velocity = new Vector2(direction * moveSpeed * Time.deltaTime * distance, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("LeftBound"))
        {
            myDireciton = Direction.Right;
        }
        else if (tag.Equals("RightBound"))
        {
            myDireciton = Direction.Left;
        }
    }
}
