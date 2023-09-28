using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPickItems : MonoBehaviour
{
    public Transform tempPoint;
    public float hookSpeed = 5.0f;
    public float stayTime = 1.5f;

    private Vector3 initialPosition;
    private bool isMoving = false;

    private void Start()
    {
        initialPosition = tempPoint.position;
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveTo(tempPoint.position);
        }
    }

    public void MoveToToy(Transform toy)
    {
        StartCoroutine(MoveToToyCoroutine(toy));
    }

    private IEnumerator MoveToToyCoroutine(Transform toy)
    {
        Vector3 targetPosition = new Vector3(toy.position.x, tempPoint.position.y, tempPoint.position.z);

        // Move to tempPoint
        MoveTo(targetPosition);
        yield return new WaitForSeconds(stayTime);

        // Move to the toy
        MoveTo(toy.position);
        yield return new WaitForSeconds(stayTime);

        // Move back to tempPoint
        MoveTo(targetPosition);
        yield return new WaitForSeconds(stayTime);

        // Move back to initial position
        MoveTo(initialPosition);

        isMoving = false;
    }

    private void MoveTo(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, hookSpeed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }
}
