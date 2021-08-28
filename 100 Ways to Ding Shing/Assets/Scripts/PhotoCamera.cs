using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PhotoCamera : MonoBehaviour
{
    [SerializeField] private GameObject flash;
    [SerializeField] private int flashCount;

    private int flashes;
    private bool isFlashing;

    private float minSeconds = .1f;
    private float maxSeconds = .35f;
    private float nextFlashSeconds;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            flashes = 0;
            StartCoroutine(cameraFlash());
        }
    }

    private IEnumerator cameraFlash()
    {
        while (flashes < flashCount)
        {
            isFlashing = !isFlashing;
            flash.SetActive(isFlashing);
            if (!isFlashing) flashes++;

            nextFlashSeconds = Random.Range(minSeconds, maxSeconds);
            yield return new WaitForSeconds(nextFlashSeconds);
        }
    }
}
