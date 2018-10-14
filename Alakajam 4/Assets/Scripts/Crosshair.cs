using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {

    [SerializeField]
    private float range = 0.5f;
    [SerializeField]
    private float aboveHead = 0.25f;

    private Angel parent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*Vector2 origo = new Vector2(parent.transform.position.x, parent.transform.position.y + aboveHead);
        Vector2 target = new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = (target - origo);
        Vector2 newPos = dir.normalized * range;

        if(dir.magnitude > range)
        {
            transform.position = new Vector3(origo.x+newPos.x, origo.y+newPos.y, transform.position.z);
        }*/
    }

    public void changePos(float X, float Y)
    {
        transform.position = new Vector3(transform.position.x + X, transform.position.y + Y, transform.position.z);
    }

    public void setPos(float X, float Y)
    {
        transform.position = new Vector3(X, Y, transform.position.z);
    }

    public void setParent(Angel parent)
    {
        this.parent = parent;
    }
}
