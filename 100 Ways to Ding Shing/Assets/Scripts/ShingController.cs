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

    private bool isMoving;
    private bool hasCollided;

    // Events
    public event System.Action OnDeath;
    public event System.Action OnCollisionAction;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        targetSprite = pointOfInterest.GetComponent<SpriteRenderer>();
        groundCheck = transform.Find("GroundCheck");
        anim = GetComponentInChildren<Animator>();
        isMoving = false;
        hasCollided = false;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // visible is still in scene view (for shadows, etc) so potentially use this instead:  https://docs.unity3d.com/ScriptReference/GeometryUtility.TestPlanesAABB.html
        float targetPosX = targetSprite.isVisible ? pointOfInterest.transform.position.x : returnPoint.transform.position.x;

        float distance = targetPosX - transform.position.x;

        velocityX = Mathf.Abs(distance) > distanceThreshold ? Mathf.Sign(distance) * speed : 0;
        isMoving = velocityX != 0 && !hasCollided;

        anim.SetBool("isMoving", isMoving);

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
        CollisionHandling(true, collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CollisionHandling(false, collision);
    }

    private void CollisionHandling(bool collided, Collision2D collision)
    {
        foreach (Transform child in collision.transform)
        {
            if (child.CompareTag("Collision Action"))
            {
                // Stop/Start moving if Kinematic (move through if it's been tinkered with)
                if (collision.gameObject.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Kinematic)
                {
                    hasCollided = collided;
                    if (collided && OnCollisionAction != null)
                    {
                        OnCollisionAction();
                    }
                }
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