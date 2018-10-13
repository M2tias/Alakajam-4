using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
{

    private float speed = 1.5f;
    private float crossHairSpeed = 2.5f;
    [SerializeField]
    private Crosshair crosshair;
    [SerializeField]
    private Bullet bulletPrefab;
    [SerializeField]
    private Bullet bullet2Prefab;

    private float timeFired = 0f;
    [SerializeField]
    private float fireDelay = 0.5f;

    // Use this for initialization
    void Start()
    {
        crosshair.setParent(this);
    }

    // Update is called once per frame
    void Update()
    {
        float inputY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        float inputX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        transform.position = new Vector3(transform.position.x + inputX, transform.position.y + inputY, transform.position.z);


        /*float targetY = Input.GetAxis("Vertical2") * Time.deltaTime * crossHairSpeed;
        float targetX = Input.GetAxis("Horizontal2") * Time.deltaTime * crossHairSpeed;
        crosshair.changePos(targetX, targetY);*/

        float targetY = Input.GetAxis("Vertical2") * 0.5f + transform.position.y;
        float targetX = Input.GetAxis("Horizontal2") * 0.5f + transform.position.x;
        crosshair.setPos(targetX, targetY + 0.5f);

        if (Input.GetButton("Fire1"))
        {
            if (Time.time - timeFired > fireDelay)
            {
                Debug.Log("Shoot!");
                Bullet bullet = Instantiate(bulletPrefab);
                bullet.transform.position = crosshair.transform.position;
                timeFired = Time.time;
            }
        }
        else if (Input.GetButton("Fire2"))
        {
            if (Time.time - timeFired > fireDelay)
            {
                Debug.Log("Alt shoot!");
                Bullet bullet = Instantiate(bullet2Prefab);
                bullet.transform.position = crosshair.transform.position;
                timeFired = Time.time;
            }
        }
    }
}
