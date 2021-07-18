using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShingController : MonoBehaviour
{
    public float speed;
    public GameObject pointOfInterest;

    Rigidbody2D rb;
    SpriteRenderer target;

    float velocityX;
    float distanceThreshold = .5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = pointOfInterest.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float distance = pointOfInterest.transform.position.x - transform.position.x;

        if (target.isVisible && Mathf.Abs(distance) > distanceThreshold) // add on ground check so position of target doesn't affect the fall in the x
        {
            float direction = Mathf.Sign(distance);
            velocityX = direction * speed;
        } else
        {
            velocityX = 0;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }
}
