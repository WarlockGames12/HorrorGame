using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playerscript : MonoBehaviour
{

    [Header("Player Settings: ")]
    public CharacterController characterController;
    public float Speed = 12f;
    public float Gravity = -9f;

    [Header("Sequences: ")]
    public Animator BeginSequence;
    private bool isSequenceDone = false;

    [Header("Bopping: ")]
    public Animator HeadBopper;

    [Header("Pause Menu")]
    public GameObject Paused;
    bool isPaused = false;

    [Header("Will play only when close to sign: ")]
    public GameObject NPC;
    public GameObject Animation;
    public GameObject PressE;
    public bool InRange = false;
    public bool isPressed = false;

    [Header("Dialogue Wont Play at beginning: ")]
    public GameObject DontPlay;
    
    [Header("Audio Walking: ")]
    public AudioSource audioSource;
    public AudioClip[] Walker;

    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        DontPlay.SetActive(false);
        characterController = GetComponent<CharacterController>();
        Paused.SetActive(false);
        PressE.SetActive(false);
        Animation.SetActive(false);
        NPC.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
            Move();
            if (!isPaused && Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
                Paused.SetActive(true);
                isPaused = true;
            }
            else if(isPaused && Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
                Paused.SetActive(false);
                isPaused = false;
            }

            if (Input.GetKeyDown(KeyCode.E) && InRange && !isPressed)
            {
                Animation.SetActive(true);
                NPC.SetActive(true);
                BeginSequence.Play("AnimationText");
                PressE.SetActive(false);
                isPressed = true;
                if (this.BeginSequence.GetCurrentAnimatorStateInfo(0).IsName("AnimationText"))
                {
                    isSequenceDone = true;
                }
                
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PressE") && !isPressed)
        {
            PressE.SetActive(true);
            InRange = true;
        }
        else
        {
            PressE.SetActive(false);
            InRange = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PressE.SetActive(false);
        InRange = false;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        Paused.SetActive(false);
        isPaused = false;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void StartBop()
    {
        HeadBopper.Play("HeadBop");
    }
    
    void StopBop()
    {
        HeadBopper.Play("New State");
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * y;
        Vector3 Conversion = transform.localPosition;
        characterController.Move(move * Speed * Time.deltaTime);

       
        velocity.y += Gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        var _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (_input.magnitude != 0)
        {
            StartBop();
        }
        else
        {
            StopBop();
        }
    }

    void CallSound()
    {
        Invoke("RandomSound", 0.2f);
    }

    void RandomSound()
    {
        audioSource.clip = Walker[Random.Range(0, Walker.Length)];
        audioSource.Play();
    }
    
}
