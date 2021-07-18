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
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (target.isVisible)
        {
            float direction = Mathf.Sign(pointOfInterest.transform.position.x - transform.position.x);
            velocity = new Vector2(direction, transform.position.y) * speed * Time.deltaTime;
        }
        else
        {
            velocity = Vector2.zero;
        }

        rb.MovePosition(rb.position + velocity);
    }
}
