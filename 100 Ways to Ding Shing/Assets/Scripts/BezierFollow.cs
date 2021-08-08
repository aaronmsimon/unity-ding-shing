using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollow : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;
    private int routeToGo;
    private float tParam;
    private Vector2 objPosition;
    private bool coroutineAllowed;

    [SerializeField] private GameObject objectToFollowPath;
    public float speedModifier = 0.5f;
    public bool loop = true;
    public float wait = 2f;

    public bool disableWhileWait;

    // Start is called before the first frame update
    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
            StartCoroutine(GoByTheRoute(routeToGo));
    }

    private IEnumerator GoByTheRoute(int routeNumber)
    {
        objectToFollowPath.SetActive(true);
        coroutineAllowed = false;

        Vector2 p0 = routes[routeNumber].GetChild(0).position;
        Vector2 p1 = routes[routeNumber].GetChild(1).position;
        Vector2 p2 = routes[routeNumber].GetChild(2).position;
        Vector2 p3 = routes[routeNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            objPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

            objectToFollowPath.transform.position = objPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo = routeToGo < routes.Length - 1 ? routeToGo++ : 0;

        if (loop)
        {
            objectToFollowPath.SetActive(!disableWhileWait);

            yield return new WaitForSeconds(wait);
            coroutineAllowed = true;
        }
    }
}
