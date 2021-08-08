using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDetector : MonoBehaviour
{
    [SerializeField] private GameObject levelComplete;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shing"))
        {
            levelComplete.SetActive(true);
        }
    }
}
