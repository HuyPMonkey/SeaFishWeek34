using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private HookController hookController;

    public Transform pointA;
    public Transform pointB;
    //public float speed = 2.0f;
    public float delayBeforeMovingToB = 3.25f;

    public GameObject BG1;
    public GameObject BG2;

    [SerializeField] private float targetTime = 1f;
    private float initialDistance;
    private float speed;

    private bool isMovingToB = false;
    //private bool isMovingToA = false;
    private bool isChangedBG = false;

    private Vector3 tempPoint;
    private Vector3 tempPoint2;

    [SerializeField] private AudioSource underWaterSound;

    void Start()
    {
        isChangedBG = false;
        initialDistance = Vector3.Distance(transform.position, pointB.position);
        speed = initialDistance / targetTime;
        Debug.Log(speed);
        
        StartCoroutine(MoveToBAfterDelay());
        hookController = FindObjectOfType<HookController>();
        tempPoint = new Vector3(BG2.transform.position.x -27, BG2.transform.position.y, BG2.transform.position.z);
        tempPoint2 = new Vector3(BG1.transform.position.x - 30, BG1.transform.position.y, BG1.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingToB)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, pointB.position) < 0.01f)
        {
            isMovingToB = false;
            underWaterSound.Play();
        }

        if(hookController.pickCountValue == 3)
        {
           
           if (Vector3.Distance(tempPoint, BG2.transform.position) > 0.01f && isChangedBG == false)
            {
                BG2.transform.position = Vector3.MoveTowards(BG2.transform.position, tempPoint, speed  * Time.deltaTime);
            }

            if (Vector3.Distance(tempPoint, BG2.transform.position) < 0.01f)
            {
                isChangedBG = true;
            }

            if (isChangedBG == true)
            {
                BG1.transform.position = Vector3.MoveTowards(BG1.transform.position, tempPoint2, speed * Time.deltaTime);
            }

        }
        
        if ( hookController.pickCountValue == 4)
        {
            hookController.pickCountValue++;
            Debug.Log(hookController.pickCountValue);
            isMovingToB = false;
            underWaterSound.Stop();
            hookController.hook.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(MoveToA());  
        }

        
    }

    private IEnumerator MoveToBAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeMovingToB);

        // Set the object to start moving towards point B
        isMovingToB = true;
    }

    private IEnumerator MoveToA()
    {
         
            while (Vector3.Distance(transform.position, pointA.position) > 0.01f)
            {
            yield return new WaitForEndOfFrame();
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed*Time.deltaTime);
            
            }
    }
    
}
