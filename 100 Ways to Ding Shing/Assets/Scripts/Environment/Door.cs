using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openedSprite;
    [SerializeField] private Vector2 closedPos;
    [SerializeField] private Vector2 openedPos;
    [SerializeField] private Vector2 closedCollider;
    [SerializeField] private Vector2 openedCollider;

    private bool isOpen;
    private SpriteRenderer sr;
    private Vector2 parentOffset;
    private BoxCollider2D bc;

    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        parentOffset = gameObject.GetComponentInParent<Transform>().position;
        bc = gameObject.GetComponent<BoxCollider2D>();

        isOpen = false;
        SetDoor();
    }

    private void OnMouseDown()
    {
        isOpen = !isOpen;
        SetDoor();
    }

    private void SetDoor()
    {
        transform.position = (isOpen ? openedPos : closedPos) + parentOffset;
        sr.sprite = isOpen ? openedSprite : closedSprite;
        bc.size = isOpen ? openedCollider : closedCollider;
    }
}
