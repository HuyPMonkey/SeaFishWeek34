using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxDoc : MonoBehaviour
{
    private MeshRenderer mRenderer;
    public float animationSpeed = 1f;

    private void Awake()
    {
        mRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        mRenderer.material.mainTextureOffset += new Vector2( 0, animationSpeed * Time.deltaTime);
    }
}
