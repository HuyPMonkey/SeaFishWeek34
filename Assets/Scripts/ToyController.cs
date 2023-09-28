    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyController : MonoBehaviour
{
    private HookPickItems hookPickItems;

    private void Start()
    {
        hookPickItems = GameObject.FindObjectOfType<HookPickItems>();
    }

    private void OnMouseDown()
    {
        if (hookPickItems != null)
        {
            hookPickItems.MoveToToy(transform);
        }
    }
}
