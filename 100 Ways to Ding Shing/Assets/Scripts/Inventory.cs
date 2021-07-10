using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in transform)
        {
            Debug.Log(child.name + " has " + (child.childCount > 0 ? child.GetChild(0).name: "nothing" ));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
