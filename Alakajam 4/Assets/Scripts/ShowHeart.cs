using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHeart : MonoBehaviour {

    [SerializeField]
    private PlayerHealth health;
    [SerializeField]
    private int showAt; //show heart if health >= showAt

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(showAt > health.CurrentHealth)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
	}
}
