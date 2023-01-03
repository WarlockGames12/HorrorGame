using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
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

    private NPCWalksToPlayerThenRuns SetTrue;
    
    [Header("Will play only when close to sign: ")]
    public GameObject NPC;
    public GameObject Animation;
    public GameObject PressE;
    public bool InRange = false;
    public bool isPressed = false;
    public bool isTalking = false;

    
    
    [Header("Dialogue Wont Play at beginning: ")]
    public GameObject DontPlay;
    
    [Header("Audio Walking: ")]
    public AudioSource audioSource;
    public AudioClip[] Walker;
    private bool _isWalking = false;

    Vector3 velocity;
    private Vector2 _footSteps;

    // Start is called before the first frame update
    private void Start()
    {
        SetTrue = FindObjectOfType<NPCWalksToPlayerThenRuns>();
        if (SetTrue == null)
        {
            Debug.Log("Do Nothing");
        }
        else
        {
            NPC.SetActive(false);
        }
        DontPlay.SetActive(false);
        characterController = GetComponent<CharacterController>();
        Paused.SetActive(false);
        PressE.SetActive(false);
        Animation.SetActive(false);
        
    }

    // Update is called once per frame
    private void Update()
    {
            Move();
            switch (isPaused)
            {
                case false when Input.GetKeyDown(KeyCode.Escape):
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Time.timeScale = 0;
                    Paused.SetActive(true);
                    isPaused = true;
                    break;
                case true when Input.GetKeyDown(KeyCode.Escape):
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    Time.timeScale = 1;
                    Paused.SetActive(false);
                    isPaused = false;
                    break;
            }

            if (Input.GetKeyDown(KeyCode.E) && SetTrue.inRangeNPC)
            {
                SetTrue.DialogueWillPlay.SetActive(true);
                isTalking = true;
            }

            if (_footSteps.magnitude != 0)
            {
                CallSound();
            }
            
            if (Input.GetKeyDown(KeyCode.E) && InRange && !isPressed)
            {
                SetTrue.animator.enabled = true;
                Animation.SetActive(true);
                NPC.SetActive(true);
                BeginSequence.Play("AnimationText");
                PressE.SetActive(false);
                isPressed = true;
                if (this.BeginSequence.GetCurrentAnimatorStateInfo(0).IsName("AnimationText"))
                {
                    isSequenceDone = true;
                    PressE.SetActive(false);
                }
                
            }

            if (SetTrue != null && SetTrue.inRangeNPC )
            {
                SetTrue.LookAtPlayer();
            }
            
            if (Input.GetKeyDown(KeyCode.E) && SetTrue.inRangeNPC)
            {
                SetTrue.DialogueWillPlay.SetActive(true);
                SetTrue._pressEToTalk.SetActive(false);
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PressE") && !isPressed && !isSequenceDone)
        {
            PressE.SetActive(true);
            InRange = true;
        }
        else
        {
            PressE.SetActive(false);
        }
        
        

        if (other.gameObject.CompareTag("PressE") && isPressed && isSequenceDone)
        {
            PressE.SetActive(false);
        }

        if (other.gameObject.CompareTag("NPC") && !isTalking)
        {
            SetTrue.inRangeNPC = true;
            SetTrue._pressEToTalk.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SetTrue.inRangeNPC = false;
        SetTrue._pressEToTalk.SetActive(false);
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

    private void StartBop()
    {
        HeadBopper.Play("HeadBop");
    }
    
    private void StopBop()
    {
        HeadBopper.Play("New State");
    }

    private void CallSound()
    {
        Invoke("randomSound", 0.3f);
    }
    
    private void randomSound()
    {
        audioSource.clip = Walker[Random.Range(0, Walker.Length)];
        audioSource.Play();
        CallSound();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Move()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var move = transform.right * x + transform.forward * y;
        characterController.Move(move * Speed * Time.deltaTime);

        velocity.y += Gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        _footSteps = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (_footSteps.magnitude != 0)
        {
            StartBop();
        }
        else
        {
            StopBop();
        }
    }

   
    
}
