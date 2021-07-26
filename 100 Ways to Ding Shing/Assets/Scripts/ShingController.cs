using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShingController : MonoBehaviour
{
    public float speed;
    public GameObject pointOfInterest;
    public LayerMask groundLayer;

    Rigidbody2D rb;
    SpriteRenderer target;
    Transform groundCheck;

    float velocityX;
    float distanceThreshold = .5f;

    bool isGrounded;
    float groundCheckRadius = .2f;

    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        target = pointOfInterest.GetComponent<SpriteRenderer>();
        groundCheck = transform.Find("GroundCheck");
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        Debug.Log(isGrounded);
        
        float distance = pointOfInterest.transform.position.x - transform.position.x;

        if (target.isVisible && Mathf.Abs(distance) > distanceThreshold)
        {
            if (isGrounded)
            {
                float direction = Mathf.Sign(distance);
                velocityX = direction * speed;
            } else
            {
                velocityX = rb.velocity.x;
            }
        } else
        {
            velocityX = 0;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
