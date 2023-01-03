using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    [Header("Transition Settings for Animation: ")]
    public Animator transition;
    public float transitionTime = 2f;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(1));
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(levelIndex);
    }
}
