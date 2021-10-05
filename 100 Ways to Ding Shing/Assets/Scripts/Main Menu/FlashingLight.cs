using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashingLight : MonoBehaviour
{
    [SerializeField] private float timeBetween = .1f;
    [SerializeField] private bool lightOn = false;

    private Light2D lightFlash;
    private float startIntensity;

    private void Start()
    {
        lightFlash = GetComponent<Light2D>();
        startIntensity = lightFlash.intensity;

        StartCoroutine(flashLight());
    }

    private IEnumerator flashLight()
    {
        while (true)
        {
            lightOn = !lightOn;
            lightFlash.intensity = lightOn ? startIntensity : 0;
            yield return new WaitForSeconds(timeBetween);
        }
    }
}
