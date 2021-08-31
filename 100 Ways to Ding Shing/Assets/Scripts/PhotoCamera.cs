using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PhotoCamera : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject flash;
    [SerializeField] private int flashCount;
    [SerializeField] private GameObject shingGraphics;
    [SerializeField] private GameObject cameraGraphics;

    private int flashes;
    private bool isFlashing;

    private float minSeconds = .1f;
    private float maxSeconds = .35f;
    private float nextFlashSeconds;

    private AudioSource audioSource;
    private ShingController shing;
    private Animator anim;
    private GameObject parentArm;

    private float armRotation = 130;
    private float armRaiseTime = .5f;
    private bool takingPicture;

    private void Awake()
    {
        shing = GameObject.FindGameObjectWithTag("Shing").transform.GetComponent<ShingController>();
    }

    private void Start()
    {
        shing.OnCollisionAction += TakePicture;
        audioSource = GetComponent<AudioSource>();
        anim = shingGraphics.GetComponent<Animator>();
        parentArm = transform.parent.gameObject;
    }

    private void TakePicture()
    {
        flashes = 0;
        StartCoroutine(moveArm());
    }

    private IEnumerator moveArm()
    {
        anim.enabled = false;

        float armSpeed = 1f / armRaiseTime;
        float percent = 0;
        Vector3 initialRotation = parentArm.transform.localEulerAngles;

        while (percent < 1)
        {
            if (!takingPicture)
            {
                percent += Time.deltaTime * armSpeed;
            }
            float interpolation = 4 * (-Mathf.Pow(percent, 2) + percent);
            float armAngle = Mathf.Lerp(0, armRotation, interpolation);
            parentArm.transform.localEulerAngles = initialRotation + Vector3.forward * armAngle;
            cameraGraphics.transform.rotation = Quaternion.Euler(Vector3.zero);

            if (percent >= .5)
            {
                takingPicture = true;
                yield return StartCoroutine(cameraFlash());
            }

            yield return null;
        }

        anim.enabled = true;
    }

    private IEnumerator cameraFlash()
    {
        while (flashes < flashCount)
        {
            Vector3 rotation = Quaternion.FromToRotation(flash.transform.position, target.transform.position).eulerAngles;
            flash.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation.z - 90f));

            isFlashing = !isFlashing;
            flash.SetActive(isFlashing);
            if (isFlashing) audioSource.Play();
            if (!isFlashing) flashes++;

            nextFlashSeconds = Random.Range(minSeconds, maxSeconds);
            yield return new WaitForSeconds(nextFlashSeconds);
        }

        takingPicture = false;
    }
}
