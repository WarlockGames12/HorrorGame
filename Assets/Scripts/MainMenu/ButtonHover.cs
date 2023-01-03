using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler
{

    public AudioSource hoverButton;
    
    public void OnPointerEnter(PointerEventData ped)
    {
        hoverButton.Play();
    }
}
