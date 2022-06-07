using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    [SerializeField] private Camera viewCamera;

    private bool isDragging;

    private float cursorRenderDistance = 3;

    private void Awake()
    {
        isDragging = false;
    }

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
            Plane backgroundPlane = new Plane(Vector3.back, Vector3.back * cursorRenderDistance);
            float rayDistance;

            if (backgroundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                transform.position = point;
            }
        }
    }
}
