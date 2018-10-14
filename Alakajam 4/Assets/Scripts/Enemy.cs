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

    private float lastShoot = 0f;
    private bool canShoot = true;
    private AudioSource screech;

    // Use this for initialization
    void Start () {
        screech = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float yWobble = Mathf.Sin(Time.time * 10) * Time.deltaTime;
        float xWiggle = Mathf.Cos(Time.time * 10) * Time.deltaTime;
        transform.position = new Vector3(transform.position.x+xWiggle, transform.position.y+yWobble, transform.position.z - scrollSpeed.Value * Time.deltaTime);

        //can shoot if canShoot, hasn't shot in ShootDelay seconds and is in shootRange units of the player
        if (canShoot && (Time.time - lastShoot > shootFrequency.ShootDelay) && (playerPosition.Pos - transform.position).magnitude < shootRange)
        {
            Bullet bullet = Instantiate(enemyBullet);
            bullet.transform.position = transform.position;
            bullet.SetDir(-(playerPosition.Pos - transform.position).normalized);
            lastShoot = Time.time;
            screech.Play();
        }
    }

    public void CanShoot(bool shoot)
    {
        canShoot = shoot;
    }
}
