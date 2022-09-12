using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        public bool isFinished { get; protected set; }
        
        protected IEnumerator WriteText(string input, Text textHolder, float Delay, AudioClip sound, float delayAfter)
        {
            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                SoundManager.Instance.PlaySound(sound);
                yield return new WaitForSeconds(Delay);
            }

            //yield return new WaitForSeconds(delayAfter);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            isFinished = true;
        }
    }
}

