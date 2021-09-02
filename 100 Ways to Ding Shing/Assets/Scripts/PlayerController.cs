using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera viewCamera;
    [SerializeField] private Transform cursor;

    private float cursorRenderDistance = 3;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane backgroundPlane = new Plane(Vector3.back, Vector3.back * cursorRenderDistance);
        float rayDistance;

        if (backgroundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            cursor.position = point;
        }
    }
}
