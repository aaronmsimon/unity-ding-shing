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
    private SpriteRenderer targetSprite;
    private Transform groundCheck;

    private float velocityX;
    private float distanceThreshold = .5f;

    private bool isGrounded;
    private float groundCheckRadius = .2f;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        targetSprite = pointOfInterest.GetComponent<SpriteRenderer>();
        groundCheck = transform.Find("GroundCheck");
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // visible is still in scene view (for shadows, etc) so potentially use this instead:  https://docs.unity3d.com/ScriptReference/GeometryUtility.TestPlanesAABB.html
        float targetPosX = targetSprite.isVisible ? pointOfInterest.transform.position.x : returnPoint.transform.position.x;

        float distance = targetPosX - transform.position.x;

        velocityX = Mathf.Abs(distance) > distanceThreshold ? Mathf.Sign(distance) * speed : 0;
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