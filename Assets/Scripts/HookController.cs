using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    public GameObject hook;
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2.0f;
    public float swayAmount = 0.1f;
    public float swaySpeed = 2.0f;

    private float startTime;
    private bool hasStartedMoving = false;

    private Transform pickedItem;

    void Start()
    {

        StartCoroutine(MoveAfterDelay());


    }

    void Update()
    {
        if (hasStartedMoving)
        {
            // Calculate the progress based on time elapsed since movement started
            float progress = (Time.time - startTime) * moveSpeed;

            // Move the object towards point B
            hook.transform.position = Vector3.Lerp(pointA.position, pointB.position, progress);
            
            // Sway the hook gently
            //SwayHook();


        }
        

    }




    private IEnumerator MoveAfterDelay()
    {
        // Wait for the initial delay
        yield return new WaitForSeconds(4f);

        // Change the Z position of the object
        Vector3 newPosition = hook.transform.position;
        newPosition.z = -4f;
        hook.transform.position = newPosition;

        // Set the start time for movement and indicate that movement has started
        startTime = Time.time;
        hasStartedMoving = true;
    }

    private void SwayHook()
    {
        
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;

        
        Vector3 newPosition = hook.transform.position;
        newPosition.x = pointB.position.x + sway;
        hook.transform.position = newPosition;
    }
}
