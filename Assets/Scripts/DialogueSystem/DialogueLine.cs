using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        [Header("Dialogue Settings: ")] 
        [SerializeField]private string input;
        private Text _textHolder;
        
        [Header("Time Parameters: ")]
        [SerializeField] private float Delay;
        [SerializeField] private float delayAfter;
        
        [Header("Sound: ")]
        [SerializeField] private AudioClip soundBeep;

        private IEnumerator lineAppear;
        
        private void Awake()
        {
            _textHolder = GetComponent<Text>();
            _textHolder.text = "";
        }

        private void OnEnable()
        {
            ResetLine();
            lineAppear = WriteText(input, _textHolder, Delay, soundBeep, delayAfter);
            StartCoroutine(lineAppear);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_textHolder.text != input)
                {
                    StopCoroutine(lineAppear);
                    _textHolder.text = input;
                }
                else
                    isFinished = true;
                
            }
        }

        private void ResetLine()
        {
            _textHolder = GetComponent<Text>();
            _textHolder.text = "";
            isFinished = false;
        }
    }
}


