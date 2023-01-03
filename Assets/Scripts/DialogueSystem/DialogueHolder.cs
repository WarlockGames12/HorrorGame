using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {

        private IEnumerator dialogueSeq;
        public bool dialogueFinished;
        
        private void OnEnable()
        {
            dialogueSeq = dialogueSequence();
            StartCoroutine(dialogueSeq);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Deactivate();
                gameObject.SetActive(false);
                StopCoroutine(dialogueSeq);
            }
        }
        
        private IEnumerator dialogueSequence()
        {
            if (!dialogueFinished)
            {
                for (var i = 0; i < transform.childCount; i++)
                {
                    Deactivate();
                    transform.GetChild(i).gameObject.SetActive(true);
                    yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().isFinished);
                }
            }
            else
            {
                var index = transform.childCount;
                Deactivate();
                transform.GetChild(index).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(index).GetComponent<DialogueLine>().isFinished);
            }

            dialogueFinished = true;
            gameObject.SetActive(false);
        }

        private void Deactivate()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}

