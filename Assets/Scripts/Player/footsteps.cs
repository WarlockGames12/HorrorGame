using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour
{

    [Header("Audio Walking: ")]
    public AudioSource audioSource;
    public CharacterController cc;

    // Start is called before the first frame update
    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        switch (cc.isGrounded)
        {
            case true when cc.velocity.magnitude > 2f && audioSource.isPlaying:
                audioSource.Play();
                break;
        }
    }
}
