using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tinker : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
