using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D myBody;
    private float fallDownSpeed = -4f;
    public float moveSpeed = 400f;
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!GameInfo.instance.end)
        {
            myBody.velocity = new Vector2(0, fallDownSpeed);         
        }
        move();
    }

    private void move()
    {
        myBody.velocity = new Vector2(Input.acceleration.x * Time.deltaTime * moveSpeed, myBody.velocity.y);

        float movement = Input.GetAxis("Horizontal");
        myBody.velocity += new Vector2(movement * moveSpeed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if(tag.Equals("DownBound") || tag.Equals("TopBound"))
        {
            if (!GameInfo.instance.end)
            {
                fallDownSpeed *= -1;
                GameInfo.instance.increaseScore();
                increaseFallSpeed();
                if (tag.Equals("DownBound"))
                {
                    SpikesRenderer.instance.renderSpikeTop();
                }
                else if (tag.Equals("TopBound"))
                {
                    SpikesRenderer.instance.renderSpikeDown();
                }
                StarSpawner.instance.tryShowStar();
            }         
            Vibrate(50);
        }

        

        if (tag.Equals("Spike"))
        {
            GameInfo.instance.endGame();
            myBody.gravityScale = 1.5f;
            myBody.mass = 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Platform"))
        {
            if(fallDownSpeed < 0)
            {
                SpikesRenderer.instance.renderSpikeTop();
            }
            else
            {
                SpikesRenderer.instance.renderSpikeDown();
            }
            fallDownSpeed *= -1;
            Vibrate(50);
        }
        if (collision.tag.Equals("Star"))
        {
            GameInfo.instance.increaseStars();
            Destroy(collision.gameObject);
            Vibrate(35);
        }
        if (collision.tag.Equals("PremiumStar"))
        {
            GameObject.Find("Game").transform.Find("Victory System").gameObject.SetActive(true);
            GameInfo.instance.increaseStars(300);
            Destroy(collision.gameObject);
            Vibrate(35);
        }
    }

    private void increaseFallSpeed()
    {
        float increaseSpeed = 0.025f;
        if(fallDownSpeed < 0)
        {
            fallDownSpeed -= increaseSpeed;
        }
        else
        {
            fallDownSpeed += increaseSpeed;
        }
    }

    private void Vibrate(int msc)
    {
        UniAndroidVibration.Vibrate(msc);
    }
}
