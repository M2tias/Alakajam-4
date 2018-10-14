using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeFrom : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float normal = 3f;
    private float black = 13f;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float value = 1 - transform.position.z / (black - normal);
        float saturation = transform.position.z / (black - normal);
        value = value > 1 ? 1 : value;
        value = value < 0 ? 0 : value;
        saturation = saturation > 1 ? 1 : saturation;
        saturation = saturation < 0 ? 0 : saturation;
        spriteRenderer.color = Color.HSVToRGB(240f / 360f, saturation, value);
    }
}
