using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEditor;

public class MoveBoat : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    [SerializeField] private float targetTime = 2.5f; 
 
    private float initialDistance;
    private float speed;
    public GameObject cat;
    private SkeletonAnimation skeAnim;

    private HookController hokcontroller;

    private Vector3 pointTemp;

    private bool isMoving = false;
    private bool isMoving2 = false;
    private bool isStopped = false;
    void Start()
    {
        hokcontroller = FindObjectOfType<HookController>();
        
        isMoving = true;
        isMoving2 = false;
        initialDistance = Vector3.Distance(pointA.position, pointB.position);
        
        pointTemp = new Vector3(pointB.position.x+ 15, pointB.position.y, pointB.position.z);
        speed = initialDistance / targetTime;
        skeAnim = GetComponent<SkeletonAnimation>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector3.Distance(transform.position, pointB.position)> 0.01f && isMoving == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, pointB.position) < 0.01f)
        {
            isMoving = false;
        }
        
            if (hokcontroller.pickCountValue == 5 && skeAnim.AnimationName == "Starting")
        {
            FindObjectOfType<AudioManager>().Play("Hooray", 3f);
            FindObjectOfType<AudioManager>().Play("Victory", 10f);
            Debug.Log("MoveBoat" + hokcontroller.pickCountValue);
            hokcontroller.pickCountValue++;
            skeAnim.AnimationName = "Ending";
            isMoving2 = true;


        }

        if (Vector3.Distance(transform.position, pointTemp) > 0.01f && isMoving2 == true && isStopped == false)
        {

            StartCoroutine(Byebye());
            transform.position = Vector3.MoveTowards(transform.position, pointTemp, speed * Time.deltaTime);
        }
    }

    private IEnumerator Byebye()
    {
        skeAnim.AnimationName = "Bien mat";
        yield return new WaitForSeconds(3f);
        isStopped = true;
    }

}
