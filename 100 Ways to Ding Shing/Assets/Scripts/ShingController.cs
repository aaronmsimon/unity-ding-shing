using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShingController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    
    [Header("Point of Interest")]
    public bool includePointOfInterest;
    [SerializeField] private GameObject pointOfInterest;
    [SerializeField] private Transform returnPoint;

    private Rigidbody2D rb;
    private SpriteRenderer target;
    private Transform groundCheck;

    private float velocityX;
    private float distanceThreshold = .5f;

    private bool isGrounded;
    private float groundCheckRadius = .2f;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        target = pointOfInterest.GetComponent<SpriteRenderer>();
        groundCheck = transform.Find("GroundCheck");
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        float distance = pointOfInterest.transform.position.x - transform.position.x;

        if (target.isVisible && Mathf.Abs(distance) > distanceThreshold)
        {
            float direction = Mathf.Sign(distance);
            velocityX = direction * speed;
        } else
        {
            velocityX = 0;
        }
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}