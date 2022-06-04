using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDetector : MonoBehaviour
{
    [SerializeField] private GameObject levelComplete;
    [SerializeField] private PlayerController player;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shing"))
        {
            audioSource.Play();
            levelComplete.SetActive(true);
            player.isAlive = false;
        }
    }
}
