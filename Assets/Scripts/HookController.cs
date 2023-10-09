using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HookController : MonoBehaviour
{
    public GameObject hook;
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2.0f;
    public float swayAmount = 0.1f;
    public float swaySpeed = 2.0f;
    public float hookPickSpeed = 2.0f;

    public int pickCountValue = 0;

    SkeletonAnimation hookSkeAnim;

    private bool hasStartedMoving = false;
    private bool isMoving = false;

    private GameObject item;

    private float startTime;
    private Vector3 targetPosition;
    private Collider2D hitCollider;

    private bool shouldSway = true;

    

    private void Start()
    {
        StartCoroutine(MoveAfterDelay());
        hookSkeAnim = GetComponent<SkeletonAnimation>();
        

    }

    private void Update()
    {
        if (hasStartedMoving)
        {
            float progress = (Time.time - startTime) * moveSpeed;
            
            hook.transform.position = Vector3.Lerp(hook.transform.position, pointB.position, progress);


            if (Vector3.Distance(hook.transform.position, pointB.position) < 0.01f)
            {
                
                hasStartedMoving = false;
                isMoving = false;
            }
        }
        
        if (!hasStartedMoving && !isMoving && Input.GetMouseButtonDown(0))
        {
            shouldSway = false;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            
            if (hitCollider != null)
            {
                item = hitCollider.gameObject;
                
                isMoving = true;
                StartCoroutine(FirstStepPickUp(item.transform.position));
                Debug.Log("Item name: " +item.gameObject.name);
            }
        }

        if(!hasStartedMoving && !isMoving && shouldSway)
        {
            SwayHook();
        }
    }

    private IEnumerator FirstStepPickUp(Vector3 target)
    {
        
        Vector3 tempPoint = new Vector3(target.x + 2, hook.transform.position.y, hook.transform.position.z);
        FindObjectOfType<AudioManager>().Play("Hook", 3f);
        while (Vector3.Distance(hook.transform.position, tempPoint) > 0.01f)
        {
            hook.transform.position = Vector3.MoveTowards(hook.transform.position, tempPoint, hookPickSpeed * Time.deltaTime);
            yield return null;



        }
        if (Vector3.Distance(hook.transform.position, tempPoint) < 0.01f)
        {
            Debug.Log("Qua Buoc 2");
            hookSkeAnim.AnimationName = "Moc gap do Open";
            yield return new WaitForSeconds(1f);
            StartCoroutine(SecondStepPickUp(item.transform.position));
        }
    }

    private IEnumerator SecondStepPickUp(Vector3 target1)
    {
        
        Vector3 tempPoint = new Vector3(target1.x +2 , target1.y -1, hook.transform.position.y);
        FindObjectOfType<AudioManager>().Play("Hook", 3f);
        while (Vector3.Distance(hook.transform.position, tempPoint) > 0.01f)
        {
            hook.transform.position = Vector3.MoveTowards(hook.transform.position, tempPoint, hookPickSpeed * Time.deltaTime);
            yield return null;
        }
        if (Vector3.Distance(hook.transform.position, tempPoint) < 0.01f)
        {
            hookSkeAnim.AnimationName = "Moc gap do Close";
            yield return new WaitForSeconds(1f);
            item.transform.SetParent(hook.transform);
            StartCoroutine(ThirdStepPickUp());
        }
            
    }

    private IEnumerator ThirdStepPickUp()
    {
        Vector3 tempPoint = new Vector3(hook.transform.position.x, hook.transform.position.y + 5, hook.transform.position.z);
        FindObjectOfType<AudioManager>().Play("Hook", 3f);
        while (Vector3.Distance(hook.transform.position,tempPoint) > 0.01f)
        {
            hook.transform.position = Vector3.MoveTowards(hook.transform.position, tempPoint,hookPickSpeed * Time.deltaTime);
            yield return  null;
        }

        if (Vector3.Distance(hook.transform.position, tempPoint) < 0.01f)
        {
            if(item.gameObject.name == "Watch")
            {
                FindObjectOfType<AudioManager>().Play("Watch", 3f);
            }
             if (item.gameObject.name == "Shoes")
            {
                FindObjectOfType<AudioManager>().Play("Sandals", 3f);
            }
             if (item.gameObject.name == "Bag")
            {
                FindObjectOfType<AudioManager>().Play("Handbag", 3f);
            }
             if (item.gameObject.name == "Hat")
            {
                FindObjectOfType<AudioManager>().Play("Hat", 3f);
            }

            yield return new WaitForSeconds(1f);
            StartCoroutine(FourthStep());
        }

    }

    private IEnumerator FourthStep()
    {
        Vector3 tempPoint = new Vector3(hook.transform.position.x, hook.transform.position.y + 5, hook.transform.position.z);

        while (Vector3.Distance(hook.transform.position, tempPoint) > 0.01f)
        {
            hook.transform.position = Vector3.MoveTowards(hook.transform.position, tempPoint, hookPickSpeed * Time.deltaTime);
            yield return null;
        }
        
        if(Vector3.Distance(hook.transform.position, tempPoint) < 0.01f)
        {
            Destroy(item);
            StartCoroutine(fifthStep());
        }
    }

    private IEnumerator fifthStep()
    {
        pickCountValue++;
        Debug.Log(pickCountValue);
        Vector3 tempPoint = new Vector3(pointB.position.x, pointB.position.y, hook.transform.position.z);
        while(Vector3.Distance(hook.transform.position, tempPoint) > 0.01f)
        {
            hook.transform.position = Vector3.MoveTowards(hook.transform.position, tempPoint, hookPickSpeed* Time.deltaTime);
            yield return null;
        }
        
        if (Vector3.Distance(hook.transform.position, tempPoint) < 0.01f)
        {
            isMoving = false;
            shouldSway = true;
        }


    }
    
    private IEnumerator MoveAfterDelay()
    {
        FindObjectOfType<AudioManager>().Play("Blue Sea (main)", 3f);
        yield return new WaitForSeconds(4f);
        
        Vector3 newPosition = hook.transform.position;
        newPosition.z = -4f;
        hook.transform.position = newPosition;
        FindObjectOfType<AudioManager>().Play("Hook", 3f);
        startTime = Time.time;
        hasStartedMoving = true;
        isMoving = true;
    }

    private void SwayHook()
    {
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        Vector3 newPosition = hook.transform.position;
        newPosition.x = pointB.position.x + sway;
        hook.transform.position = newPosition;
    }


}
