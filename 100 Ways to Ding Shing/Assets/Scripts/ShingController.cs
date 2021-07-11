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
        if (target.isVisible)
        {
            Vector2 direction = (pointOfInterest.transform.position - transform.position).normalized;
            velocity = new Vector2(direction.x, 0) * speed * Time.deltaTime;
        } else
        {
            velocity = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition((Vector2)transform.position + velocity);
    }
}
