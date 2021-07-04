using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShingController : MonoBehaviour
{
    public float speed;
    public GameObject pointOfInterest;

    Rigidbody2D rb;
    SpriteRenderer target;
    Vector2 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = pointOfInterest.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Debug.Log(target.isVisible);
        if (target.isVisible)
        {
            Vector2 moveDirection = (pointOfInterest.transform.position - transform.position).normalized;
            velocity = moveDirection * speed;
        } else
        {
            velocity = new Vector2(0, 0);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(velocity.x, rb.velocity.y);
    }
}
