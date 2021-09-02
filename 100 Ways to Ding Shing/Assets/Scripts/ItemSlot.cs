using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    private Sprite item;

    private void Start()
    {
        item = GetComponent<Image>().sprite;        
    }

    public void ChangeCursor()
    {
        CursorController.Instance.ChangeCursor(item);
    }
}
