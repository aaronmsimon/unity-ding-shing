using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbulanceSceneManager : MonoBehaviour
{
    /* Shing */
    [SerializeField] private Transform shing;
    [SerializeField] private float shingTimeToTarget = 2;

    private Vector3 shingStartPos;
    private Vector3 shingTargetPos = new Vector3(1.67f, - 0.23f);
    private SpriteRenderer shingSprite;

    private Vector3 shingStartScale;
    private Vector3 shingEndScale = Vector3.one;

    /* Ambulance */
    [SerializeField] private Transform ambulance;
    [SerializeField] private float ambulanceWaitPercent = .5f;

    private Vector3 ambulanceStartPos;
    private Vector3 ambulanceTargetPos = new Vector3(1.67f, -0.23f);
    private SpriteRenderer ambulanceSprite;
    private bool ambulanceStarted = false;

    private void Start()
    {
        shingSprite = shing.GetComponent<SpriteRenderer>();
        shingStartPos = shing.position;
        shingStartScale = shing.localScale;
        StartCoroutine(ShingRuns());

        ambulanceSprite = ambulance.GetComponent<SpriteRenderer>();
        ambulanceStartPos = ambulance.position;
        ambulanceTargetPos = new Vector3(shingTargetPos.x + ambulanceSprite.size.x / 2, shingTargetPos.y - shingSprite.size.y / 2 + ambulanceSprite.size.y / 2, 0);
    }

    private IEnumerator ShingRuns()
    {
        float moveSpeed = 1f / shingTimeToTarget;
        float percent = 0;

        while (percent <= 1)
        {
            if (percent >= ambulanceWaitPercent && !ambulanceStarted)
            {
                StartCoroutine(AmbulanceDrives(shingTimeToTarget * (1 - percent)));
                ambulanceStarted = true;
            }

            shing.position = Vector3.Lerp(shingStartPos, shingTargetPos, percent);
            shing.localScale = Vector3.Lerp(shingStartScale, shingEndScale, percent);

            percent += Time.deltaTime * moveSpeed;
            yield return null;
        }
    }

    private IEnumerator AmbulanceDrives(float timeToTarget)
    {
        float moveSpeed = 1f / timeToTarget;
        float percent = 0;

        while (percent <= 1)
        {
            ambulance.position = Vector3.Lerp(ambulanceStartPos, ambulanceTargetPos, percent);

            percent += Time.deltaTime * moveSpeed;
            yield return null;
        }
    }
}
