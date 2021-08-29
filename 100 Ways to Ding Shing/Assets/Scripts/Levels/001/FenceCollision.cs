using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceCollision : MonoBehaviour
{
    [SerializeField] private GameObject fence;
    [SerializeField] private GameObject dirtKickup;

    private Rigidbody2D rbFence;

    private void Start()
    {
        rbFence = fence.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Shing" && rbFence.bodyType == RigidbodyType2D.Dynamic)
        {
            dirtKickup.transform.parent = null;
            dirtKickup.GetComponent<ParticleSystem>().Play();
        }
    }
}
