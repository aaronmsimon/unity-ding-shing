using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject defaultOption;

    void Update()
    {
        // if menu items are out of focus, select the default when any arrow is pressed
        if (EventSystem.current.currentSelectedGameObject == null & (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            EventSystem.current.SetSelectedGameObject(defaultOption);
        }
    }
}
