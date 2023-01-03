using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace DialogueSystem
{
    public class NPCWalksToPlayerThenRuns : MonoBehaviour
     {

       [Header("WayPoints & Speed: ")]
       public Transform wayPoints;
       public Transform wayPoint;
       public int speed;
       public Transform lookOutPoint;
       
       [Header("Animators: ")]
       public Animator animator;
       
       [Header("GameObjects of NPC: ")]
       public GameObject thisGameObject;
       public GameObject idle;
       public GameObject runningNpc;
       
       [Header("Transforms from NPC & Player: ")]
       public Transform Idles;
       public Transform Player;

       [Header("Other Scripts: ")]
       public Playerscript isTrue;
       public DialogueHolder Bool;
       public DialogueHolder Bool2;
       

       [Header("Bools: ")]
       public bool inRangeNPC = false;

       [Header("Dialogue will Play: ")] 
       public GameObject _pressEToTalk;
       public GameObject Info;
       public GameObject DialogueWillPlay;
       public GameObject DialogueWillPlay1;
       public GameObject DialogueWillPlay2;
       
       [Header("Bus Arrives: ")] 
       public GameObject busGoesWeee;
       [SerializeField] private AudioSource BusStops;
       [SerializeField] private AudioClip stopNowPlease;
       [SerializeField] private AudioSource busHasStopped;
       public Transform busGoesToTransform;
       public BoxCollider goesOffForBus;
       [SerializeField] private GameObject deathEnding1;
    
       //Waypoints & Distance
       private int _wayPointIndex;
       private float _dist;
       public int _busNeedsToStop = 0;
       

       private void Start()
       {
           animator.enabled = false;
           _pressEToTalk.SetActive(false);
           Info.SetActive(false);
           DialogueWillPlay.SetActive(false);
           DialogueWillPlay1.SetActive(false);
           DialogueWillPlay2.SetActive(false);
           busGoesWeee.SetActive(false);
           deathEnding1.SetActive(false);

           switch (animator.enabled)
           {
               case true:
                   animator.Play("mixamo_com Walk");
                   switch (isTrue.isPressed)
                   {
                       //Start at 0 & look at the 1st waypoint
                       case true:
                           _wayPointIndex = 0;
                           transform.LookAt(wayPoints.position);
                           thisGameObject.SetActive(true);
                           idle.SetActive(false);
                           LookAtPlayer();
                           break;
                   }
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
                    if (isTrue.isPressed)
                    {
                        _dist = Vector3.Distance(thisGameObject.transform.position, wayPoints.transform.position);
                        if (_dist < 2f)
                        {
                            IncreaseIndex();
                        }
                        Patrol();
                    }

                    break;
                }
               case 1:
                   thisGameObject.SetActive(false);
                   idle.SetActive(true);
                   speed = 0;
                   thisGameObject.transform.LookAt(lookOutPoint.position);
                   break;
               default:
                   thisGameObject.SetActive(false);
                   idle.SetActive(true);
                   speed = 0;
                   thisGameObject.transform.LookAt(lookOutPoint.position);
                   DialogueWillPlay.SetActive(true);
                   break;
           }

           switch (Bool.dialogueFinished)
           {
               case true:
               {
                   idle.SetActive(false);
                   runningNpc.SetActive(true);
                   var newDistance = Vector3.Distance(runningNpc.transform.position, wayPoint.position);
                   Patrol2();
                   if (newDistance < 2)
                   {
                       runningNpc.SetActive(false);
                       DialogueWillPlay1.SetActive(true);
                   }
                   break;
               }
           }
           
           switch (Bool2.dialogueFinished)
           {
               case true:
               {
                   busGoesWeee.SetActive(true);
                   if (busGoesWeee.activeSelf)
                   {
                       if (_busNeedsToStop == 0)
                       {
                           deathEnding1.SetActive(true);
                           var distanceTobus = Vector3.Distance(busGoesWeee.transform.position, busGoesToTransform.position);
                           Patrol3();
                           if (distanceTobus < 7)
                           { 
                               BusStops.PlayOneShot(stopNowPlease);
                               _busNeedsToStop++;
                               if (busHasStopped.isPlaying == false)
                               {
                                   busHasStopped.Play();
                                   deathEnding1.SetActive(false);
                               }
                           }
                       }
                   }
                   break;
               }

                  
           }
          
       }

       private void OnCollisionEnter()
       {
           goesOffForBus.GetComponent<BoxCollider>().enabled = !busGoesWeee.CompareTag("InvisableWall");
       }

       private void OnCollisionExit()
       {
           goesOffForBus.GetComponent<BoxCollider>().enabled = true;
       }

       private void OnTriggerEnter(Collider other)
       {
           if (other.gameObject.CompareTag("Player"))
           {
               inRangeNPC = true;
               Info.SetActive(false);
               _pressEToTalk.SetActive(true);
           }
       }

       private void OnTriggerExit(Collider other)
       {
           inRangeNPC = false;
           _pressEToTalk.SetActive(false);
       }

       public void LookAtPlayer()
       {
            var targetPosition = new Vector3(Player.position.x, this.transform.position.y, Player.position.z ) ;
            Idles.transform.LookAt(targetPosition);
        }

       
       
       private void Patrol()
       {
           speed = 2;
           thisGameObject.transform.Translate(Vector3.forward * (speed * Time.deltaTime));
       }
       
       private void Patrol2()
       {
           speed = 4;
           runningNpc.transform.Translate(Vector3.forward * (speed * Time.deltaTime));
       }

       private void Patrol3()
       {
           speed = 6;
           busGoesWeee.transform.Translate(Vector3.forward * (speed * Time.deltaTime));
       }

       private void IncreaseIndex()
       {
           _wayPointIndex++;
           Idles.transform.position = wayPoints.transform.position;
           idle.transform.LookAt(lookOutPoint.transform.position);
       }
    
     }

}
