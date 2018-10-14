using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInLayer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z <= -5)
        {
            spriteRenderer.sortingOrder = 4;
        }
        else
        {
            spriteRenderer.sortingOrder = -(int)Mathf.Floor((transform.position.z + 5) / 3f);
        }
    }
}
