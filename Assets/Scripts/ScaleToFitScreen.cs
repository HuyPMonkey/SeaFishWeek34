using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToFitScreen : MonoBehaviour
{
    private SpriteRenderer sr;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        float worldScreenHeight = Camera.main.orthographicSize * 2;

        float worldScreenWitdh = worldScreenHeight/ Screen.height * Screen.width;

        transform.localScale = new Vector3(worldScreenWitdh / sr.sprite.bounds.size.x, worldScreenHeight / sr.sprite.bounds.size.y, 1);
    }
}
