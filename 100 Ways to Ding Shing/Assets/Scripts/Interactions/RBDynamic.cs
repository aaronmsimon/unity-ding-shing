using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBDynamic : MonoBehaviour
{
    [SerializeField] private Sprite item;

    private Rigidbody2D rb;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        if (CursorController.spriteRenderer.sprite == item)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            audioSource.Play();
        }
    }
}
