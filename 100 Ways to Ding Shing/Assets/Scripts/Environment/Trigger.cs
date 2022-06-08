using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public event System.Action OnContainsItem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null && OnContainsItem != null)
        {
            OnContainsItem();
        }
    }
}
