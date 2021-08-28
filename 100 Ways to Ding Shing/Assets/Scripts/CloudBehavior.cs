using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehavior : MonoBehaviour
{
    public float speed = 0;

    private SpriteRenderer rend;
    private float width;
    private float screenWidth;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        width = rend.bounds.size.x;
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    private void Update()
    {
        if (transform.position.x < screenWidth + width / 2f)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        } else
        {
            transform.position = new Vector2(-screenWidth - width / 2f, transform.position.y);
        }
    }
}