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
    private Animator anim;

    private float velocityX;
    private float distanceThreshold = .5f;

    private bool isGrounded;
    private float groundCheckRadius = .2f;

    private float lastPosX;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        targetSprite = pointOfInterest.GetComponent<SpriteRenderer>();
        groundCheck = transform.Find("GroundCheck");
        anim = GetComponentInChildren<Animator>();
        lastPosX = transform.position.x;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // visible is still in scene view (for shadows, etc) so potentially use this instead:  https://docs.unity3d.com/ScriptReference/GeometryUtility.TestPlanesAABB.html
        float targetPosX = targetSprite.isVisible ? pointOfInterest.transform.position.x : returnPoint.transform.position.x;

        float distance = targetPosX - transform.position.x;

        velocityX = Mathf.Abs(distance) > distanceThreshold ? Mathf.Sign(distance) * speed : 0;

        float traveledSinceLast = Mathf.Abs(lastPosX - transform.position.x);
        anim.SetFloat("Speed", traveledSinceLast > 0.01 ? Mathf.Abs(velocityX) : 0);
        lastPosX = transform.position.x;
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (Transform child in collision.transform)
        {
            if (child.CompareTag("Collision Action"))
            {
                Debug.Log("Take picture");
                // I think I can put a script on the collider that triggered this to broadcast a message
                // then the camera would listen for that message and take a picture
                // hopefully I can figure out how to get the camera connected to the broadcast via public variable
            }
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