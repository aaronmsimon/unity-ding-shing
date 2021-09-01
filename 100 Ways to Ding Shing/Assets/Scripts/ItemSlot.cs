using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Texture2D cursor;
    [SerializeField] private bool middleHotspot;

    public void ChangeCursor()
    {
        CursorController.Instance.ChangeCursor(cursor, middleHotspot);
    }
}
