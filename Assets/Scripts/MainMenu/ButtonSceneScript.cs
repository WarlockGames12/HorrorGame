using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneScript : MonoBehaviour
{

    [Header("Sequence & Tutorial: ")]
    public GameObject Buttons;
    public Animator TitleScreen;
    public float FadeSpeed;
    public Animator Tutorials;
    public Animator TutorialScreen;
    public Animator CameraZoom;

    [Header("Bus Arrives: ")]
    public AudioSource BusArrives;
    public Animator Bus;
    public AudioSource WRRRR;
    bool DontPlay = false;

    bool stopFading = false;


    private void Start()
    {
        TutorialScreen.GetComponent<CanvasGroup>().alpha = 0f;
        switch (stopFading)
        {
            case false:
                CameraZoom.GetComponent<Animator>().enabled = false;
                Tutorials.GetComponent<Animator>().enabled = false;
                TutorialScreen.GetComponent<Animator>().enabled = false;
                TitleScreen.GetComponent <Animator>().enabled = false;
                Bus.GetComponent<Animator>().enabled = false;
                break;
        }
    }

    public void StartNewGame()
    {
        TitleScreen.GetComponent<Animator>().enabled = true;
        CameraZoom.GetComponent<Animator>().enabled = true;
        TutorialScreen.GetComponent<CanvasGroup>().alpha = 1f;
        Tutorials.GetComponent<Animator>().enabled = true;
        Bus.GetComponent<Animator>().enabled = true;
        BusArrives.Play();
        Buttons.SetActive(false);
        DontPlay = true;
        if (BusArrives.isPlaying == false && DontPlay)
        {
            WRRRR.Play();
        }
       
    }

    public void NewGame()
    {
        TutorialScreen.GetComponent <Animator>().enabled = true;
        SceneManager.LoadScene("SampleScene");
    }

    

    public void ExitGame()
    {
        Application.Quit();
    }

}
