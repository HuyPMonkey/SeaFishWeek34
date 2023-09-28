using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2.0f;

    private Transform target;

    private float originalY;

    void Start()
    {
        target = pointB;
        originalY = transform.position.y;   
    }


    void Update()
    {
        Vector3 targetPosition = new Vector3(target.position.x, originalY, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);


        if (transform.position == targetPosition)
        {
            // Flip the object by changing the x-scale
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

            if (target == pointA)
                target = pointB;
            else
                target = pointA;
        }
    }
}
