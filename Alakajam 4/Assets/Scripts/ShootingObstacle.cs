using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingObstacle : MonoBehaviour
{
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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot && (Time.time - lastShoot > shootFrequency.ShootDelay) && (playerPosition.Pos - transform.position).magnitude < shootRange)
        {
            Bullet bullet = Instantiate(enemyBullet);
            bullet.transform.position = transform.position + Vector3.up * 0.38f;
            bullet.SetDir(-(playerPosition.Pos - transform.position).normalized - Vector3.down * 0.1f);
            lastShoot = Time.time;
        }
    }
}
