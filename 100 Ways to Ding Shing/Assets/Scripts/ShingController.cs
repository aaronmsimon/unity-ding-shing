using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShingController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float fallDistThreshold;

    [Header("Point of Interest")]
    public bool includePOI;
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
    private bool isFalling;
    private bool hasCollided;

    // Events
    public event System.Action OnDeath;
    public event System.Action OnCollisionAction;
    public event System.Action OnFalling;

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

        CheckIfFalling();

        // visible is still in scene view (for shadows, etc) so potentially use this instead:  https://docs.unity3d.com/ScriptReference/GeometryUtility.TestPlanesAABB.html
        float targetPosX = targetSprite.isVisible ? pointOfInterest.transform.position.x : returnPoint.transform.position.x;
        float distance = targetPosX - transform.position.x;
        velocityX = Mathf.Abs(distance) > distanceThreshold ? Mathf.Sign(distance) * speed : 0;

        isMoving = velocityX != 0 && !hasCollided;

        // Face move direction
        transform.localScale = new Vector3(Mathf.Sign(velocityX), transform.localScale.y);

        anim.SetBool("isFalling", isFalling);
        anim.SetBool("isMoving", !isFalling ? isMoving : false);
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

    private void CheckIfFalling()
    {
        RaycastHit2D ray = Physics2D.Raycast(groundCheck.position + Vector3.down * .1f, Vector2.down, 10f);
        isFalling = ray.distance >= fallDistThreshold;

        if (isFalling)
        {
            if (OnFalling != null)
                OnFalling();
            // want to have Shing drop anything he's holding - will come back to this (removed the rb on camera)
            //transform.Find("Graphics").Find("Arm.R").DetachChildren();
            //transform.Find("Graphics").Find("Arm.L").DetachChildren();
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