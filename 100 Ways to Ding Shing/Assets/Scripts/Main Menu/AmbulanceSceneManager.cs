using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbulanceSceneManager : MonoBehaviour
{
    /* Shing */
    [Header("Shing")]
    [SerializeField] private Transform shing;
    [SerializeField] private float shingTimeToTarget = 2;

    private Vector3 shingStartPos;
    private SpriteRenderer shingSprite;

    private Vector3 shingStartScale;
    private Vector3 shingEndScale = Vector3.one;

    /* Ambulance */
    [Header("Ambulance")]
    [SerializeField] private Transform ambulance;
    [SerializeField] private float ambulanceWaitPercent = .5f;

    private Vector3 ambulanceStartPos;
    private Vector3 ambulanceTargetPos = new Vector3(1.67f, -0.23f);
    private SpriteRenderer ambulanceSprite;
    private bool ambulanceStarted = false;

    /* Collision */
    [Header("Collision")]
    [SerializeField] private Transform collisionPos;
    [SerializeField] private float collisionRotateTime = 1;
    [SerializeField] private float collisionFinalZRotation = 90;
    [SerializeField] private float collisionFlyTime = 1;
    [SerializeField] private Sprite deadShing;
    [SerializeField] private Transform[] collisionPaths;

    private void Start()
    {
        shingSprite = shing.GetComponent<SpriteRenderer>();
        shingStartPos = shing.position;
        shingStartScale = shing.localScale;
        StartCoroutine(ShingRuns());

        ambulanceSprite = ambulance.GetComponent<SpriteRenderer>();
        ambulanceStartPos = ambulance.position;
        float offset = 0;
        ambulanceTargetPos = new Vector3(collisionPos.position.x + ambulanceSprite.size.x / 2 + offset, collisionPos.position.y - shingSprite.size.y / 2 + ambulanceSprite.size.y / 2);
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

            shing.position = Vector3.Lerp(shingStartPos, collisionPos.position, percent);
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

        shing.GetComponent<Animator>().enabled = false;
        shing.GetComponent<SpriteRenderer>().sprite = deadShing;
        StartCoroutine(ShingRotates());
        StartCoroutine(ShingFollowPath());
    }

    private IEnumerator ShingRotates()
    {
        float rotateSpeed = 1f / collisionRotateTime;
        float percent = 0;
        Vector3 initialRotation = shing.localEulerAngles;

        while (percent <= 1)
        {
            float bodyAngle = Mathf.Lerp(0, collisionFinalZRotation, percent);
            shing.localEulerAngles = initialRotation + Vector3.forward * bodyAngle;

            percent += Time.deltaTime * rotateSpeed;
            yield return null;
        }
    }

    private IEnumerator ShingFollowPath()
    {
        float flySpeed = 1f / collisionFlyTime;

        for (int i = 0; i < collisionPaths.Length; i++)
        {
            float percent = 0;
            Vector3 startPos = shing.position;
            while (percent <= 1)
            {
                percent += Time.deltaTime * flySpeed;
                shing.position = Vector3.Lerp(startPos, collisionPaths[i].position, percent);
                yield return null;
            }
        }
    }
}
