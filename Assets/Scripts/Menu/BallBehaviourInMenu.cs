using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviourInMenu : MonoBehaviour
{
    private Rigidbody2D myBody;
    private const float speed = 400f;
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        myBody.velocity = new Vector2(Input.acceleration.x * Time.deltaTime * speed, myBody.velocity.y);
        myBody.gravityScale = -Input.acceleration.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        UniAndroidVibration.Vibrate(50);
    }

}
