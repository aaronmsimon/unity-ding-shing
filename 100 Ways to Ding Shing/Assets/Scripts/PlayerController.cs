using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera viewCamera;
    [SerializeField] private Transform cursor;
    [SerializeField] private Animator transition;

    private float cursorRenderDistance = 3;

    [HideInInspector] public bool isAlive;

    private void Start()
    {
        Cursor.visible = false;
        isAlive = true;
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

        if (Input.GetButton("Fire1") && !isAlive)
        {
            Cursor.visible = true;
            StartCoroutine(GoToMainMenu());
        }
    }

    IEnumerator GoToMainMenu()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Ambulance");
    }
}
