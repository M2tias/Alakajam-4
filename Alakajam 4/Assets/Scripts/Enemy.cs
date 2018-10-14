using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private ScrollSpeed scrollSpeed;
    [SerializeField]
    private Bullet enemyBullet;
    [SerializeField]
    private ShootFrequency shootFrequency;
    [SerializeField]
    private Position playerPosition;
    [SerializeField]
    private float shootRange = 5f;
    [SerializeField]
    private bool log = false;

    private float lastShoot = 0f;
    private bool canShoot = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float yWobble = Mathf.Sin(Time.time * 10) * Time.deltaTime;
        float xWiggle = Mathf.Cos(Time.time * 10) * Time.deltaTime;
        //Debug.Log(lol);
        transform.position = new Vector3(transform.position.x+xWiggle, transform.position.y+yWobble, transform.position.z - scrollSpeed.Value * Time.deltaTime);

        //can shoot if canShoot, hasn't shot in ShootDelay seconds and is in shootRange units of the player
        if(log) { Debug.Log((playerPosition.Pos - transform.position).magnitude);  }
        if (canShoot && (Time.time - lastShoot > shootFrequency.ShootDelay) && (playerPosition.Pos - transform.position).magnitude < shootRange)
        {
            Bullet bullet = Instantiate(enemyBullet);
            bullet.transform.position = transform.position;
            bullet.SetDir(-(playerPosition.Pos - transform.position).normalized);
            lastShoot = Time.time;
        }
    }

    public void CanShoot(bool shoot)
    {
        canShoot = shoot;
    }
}
