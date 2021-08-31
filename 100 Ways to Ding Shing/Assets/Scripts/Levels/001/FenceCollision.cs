using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceCollision : MonoBehaviour
{
    [SerializeField] private GameObject fence;
    [SerializeField] private GameObject dirtKickup;

    private ShingController shing;
    private Rigidbody2D rbFence;

    private float force = 5f;

    private void Start()
    {
        rbFence = fence.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Shing" && rbFence.bodyType == RigidbodyType2D.Dynamic)
        {
            rbFence.AddForce(Vector2.right * force, ForceMode2D.Impulse);

            dirtKickup.transform.parent = null;
            dirtKickup.transform.localScale = Vector3.one;
            dirtKickup.GetComponent<ParticleSystem>().Play();
        }
    }
}
