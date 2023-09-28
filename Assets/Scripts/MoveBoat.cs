using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoat : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    [SerializeField] private float targetTime = 2.5f; 

    private float initialDistance;
    private float speed;

    
    void Start()
    {
        
        initialDistance = Vector3.Distance(pointA.position, pointB.position);

       
        speed = initialDistance / targetTime;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
    }
}
