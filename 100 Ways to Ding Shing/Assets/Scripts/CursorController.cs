using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance { get { return _instance; } }
    public static string activeItemName;

    private static CursorController _instance;
    private static SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public void ChangeCursor(Sprite item)
    {
        spriteRenderer.sprite = item;
        activeItemName = item.name;
    }
}
