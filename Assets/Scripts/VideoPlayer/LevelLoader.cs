using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelLoader : MonoBehaviour
{

    [Header("Transition Settings for Animation: ")]
    public Animator transition;
    public float transitionTime = 2f;
    public string sceneName;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}
