using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    //public float speed = 2.0f;
    public float delayBeforeMovingToB = 3.25f;

    [SerializeField] private float targetTime = 1f;
    private float initialDistance;
    private float speed;

    private bool isMovingToB = false;

    void Start()
    {
        initialDistance = Vector3.Distance(transform.position, pointB.position);
        speed = initialDistance / targetTime;
        StartCoroutine(MoveToBAfterDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingToB)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
        }
    }

    private IEnumerator MoveToBAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeMovingToB);

        // Set the object to start moving towards point B
        isMovingToB = true;
    }
}
