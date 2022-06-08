using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShingController : MonoBehaviour
{
    public enum ShingBehavior { Idle, Meander, Target }

    [Header("Behavior")]
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private ShingBehavior behavior;
    [SerializeField] private float meanderTime;
    [SerializeField] private Vector2 meanderXMinMax;
    [SerializeField] private Transform moveToTarget;

    private Rigidbody2D rb;
    private Transform groundCheck;
    private Animator anim;

    private bool isGrounded;
    private float groundCheckRadius = .2f;

    private Vector3 target;
    private bool isMoving;

    // Events
    public event System.Action OnCollisionAction;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
        anim = GetComponentInChildren<Animator>();

        if (behavior == ShingBehavior.Meander)
        {
            StartCoroutine(Meander());
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        switch (behavior)
        {
            case ShingBehavior.Idle:
                target = transform.position;
                break;

            case ShingBehavior.Meander:
                break;

            case ShingBehavior.Target:
                target = moveToTarget.position;
                break;
        }

        anim.SetBool("isMoving", isMoving);
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2((target - transform.position).normalized.x * speed, 0);
        }
    }

    IEnumerator Meander()
    {
        while (behavior == ShingBehavior.Meander)
        {
            yield return new WaitForSeconds(meanderTime);
            target = new Vector3(Random.Range(meanderXMinMax.x, meanderXMinMax.y), transform.position.y);
            Debug.Log(target);
        }
    }
}