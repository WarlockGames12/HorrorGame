using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class NPCWalksToPlayerThenRuns : MonoBehaviour
{

    [Header("WayPoints & Speed: ")]
    public Transform[] wayPoints;
    public int speed;
    public Transform lookOutPoint;
    public Animator animator;
    public GameObject thisGameObject;
    public GameObject idle;
    public Transform Player;

    [Header("Other Scripts: ")]
    public Playerscript isTrue;
    public DialogueHolder Bool;
    

    [Header("Dialogue will Play: ")] 
    public GameObject DialogueWillPlay;
    
    //Waypoints & Distance
    private int _wayPointIndex;
    private float _dist;

    private void Start()
    {
        DialogueWillPlay.SetActive(false);
        switch (isTrue.isPressed)
        {
            //Start at 0 & look at the 1st waypoint
            case true:
                _wayPointIndex = 0;
                transform.LookAt(wayPoints[_wayPointIndex].position);
                thisGameObject.SetActive(true);
                idle.SetActive(false);
                animator.Play("mixamo_com Walk");
                break;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        switch (_wayPointIndex)
        {
            case 0:
            {
                _dist = Vector3.Distance(transform.position, wayPoints[_wayPointIndex].position);
                if (_dist < 2f)
                {
                    IncreaseIndex();
                }
                Patrol();
                break;
            }
            case 1:
                thisGameObject.SetActive(false);
                idle.SetActive(true);
                speed = 0;
                transform.LookAt(lookOutPoint.position);
                animator.Play("mixamo.com idle");
                DialogueWillPlay.SetActive(true);
                break;
            default:
                speed = 0;
                transform.LookAt(lookOutPoint.position);
                break;
        }
    }

    private void LookAtPlayer()
    {
        var currentPosition = transform.position;
        var RotationSpeed = 3f;
        var inputVector3 = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        inputVector3 += currentPosition;

        Quaternion targetRotation = Quaternion.LookRotation(inputVector3 - currentPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed);

    }
    
    private void Patrol()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    private void IncreaseIndex()
    {
        _wayPointIndex++;
        if(_wayPointIndex >= wayPoints.Length)
        {
            _wayPointIndex = 0;
        }
        transform.LookAt(wayPoints[_wayPointIndex].position);
    }
    
}
