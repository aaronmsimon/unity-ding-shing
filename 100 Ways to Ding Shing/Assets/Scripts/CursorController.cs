using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance { get { return _instance; } }
    public static string activeCursorName;

    private static CursorController _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetCursor();
        }
    }

    public void ChangeCursor(Texture2D cursor, bool useMiddle)
    {
        Vector2 hotspot = new Vector2(cursor.width / 2, cursor.height / 2);
        Cursor.SetCursor(cursor, useMiddle ? hotspot : Vector2.zero, CursorMode.Auto);
        activeCursorName = cursor.name;
    }

    private void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
