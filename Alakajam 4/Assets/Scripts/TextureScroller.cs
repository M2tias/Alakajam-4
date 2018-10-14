using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroller : MonoBehaviour {

    [SerializeField]
    private Material fieldMaterial;
    [SerializeField]
    private Renderer _renderer;
    private float maxScroll = 0.25f;
    private float currentScroll = 0f;
    [SerializeField]
    private float scrollSpeed = 20f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentScroll += -(scrollSpeed / 100f) * Time.deltaTime;
        if(currentScroll >= maxScroll)
        {
            currentScroll = 0f;
        }
        //fieldMaterial.SetTextureOffset("_MainTex", new Vector2(0, currentScroll));
        _renderer.material.SetTextureOffset("_MainTex", new Vector2(0, currentScroll)); //mainTextureOffset = new Vector2(0, currentScroll);
        //scrollSpeed *= 1.01f; //lol
	}

    public void SetScrollSpeed(float speed)
    {
        scrollSpeed = speed;
    }
}
