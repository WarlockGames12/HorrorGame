using System;
using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;

public class PlayerDeathEnding : MonoBehaviour
{


    [Header("Player Dies Dialogue")] 
    [SerializeField] private AudioSource DeathSound;
    [SerializeField] private GameObject DialogueText;
    [SerializeField] private GameObject OnlyDialogue;
    [SerializeField] private DialogueHolder holder;
    [SerializeField] private NPCWalksToPlayerThenRuns busStop;
    [SerializeField] private GameObject videoPlaysCredits;
    [SerializeField] private AudioSource[] rainStop;

    private void Start()
    {
        DialogueText.SetActive(false);
        videoPlaysCredits.SetActive(false);
        OnlyDialogue.SetActive(false);
    }

    private void Update()
    {
        if (holder.dialogueFinished)
        {
            OnlyDialogue.SetActive(false);
            videoPlaysCredits.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (var i = 0; i < rainStop.Length; i++)
            {
                rainStop[i].Stop();
            }
            DeathSound.Play();
            DialogueText.SetActive(true);
            OnlyDialogue.SetActive(true);
            busStop._busNeedsToStop = 1;
        }
    }
}
