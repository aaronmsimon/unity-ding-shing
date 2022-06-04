using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private string levelID;
    [SerializeField] private string levelName;
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1f;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(LoadLevel);
        gameObject.GetComponentInChildren<Text>().text = levelName;
    }

    void LoadLevel()
    {
        StartCoroutine(LoadLevelSetup());
    }

    IEnumerator LoadLevelSetup()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("Level" + levelID);
    }
}
