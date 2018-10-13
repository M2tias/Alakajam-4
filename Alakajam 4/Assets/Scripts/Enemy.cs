using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private ScrollSpeed scrollSpeed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float lol = Mathf.Sin(Time.time * 10) * Time.deltaTime;
        float lel = Mathf.Cos(Time.time * 10) * Time.deltaTime;
        Debug.Log(lol);
        transform.position = new Vector3(transform.position.x+lel, transform.position.y+lol, transform.position.z - scrollSpeed.Value * Time.deltaTime);
    }
}
