using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance { get { return _instance; } }

    public static SpriteRenderer spriteRenderer;

    private static CursorController _instance;
    private static Sprite defaultSprite;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
            spriteRenderer = GetComponent<SpriteRenderer>();
            defaultSprite = spriteRenderer.sprite;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetCursor();
        }
    }

    public void ChangeCursor(Sprite item)
    {
        spriteRenderer.sprite = item;
    }

    public void ResetCursor()
    {
        spriteRenderer.sprite = defaultSprite;
    }
}