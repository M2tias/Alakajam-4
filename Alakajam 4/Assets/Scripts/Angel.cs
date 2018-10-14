using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Angel : MonoBehaviour
{

    private float speed = 1.5f;
    private float crossHairSpeed = 2.5f;
    [SerializeField]
    private Crosshair crosshair;
    [SerializeField]
    private PlayerHealth health;
    [SerializeField]
    private Bullet bulletPrefab;
    [SerializeField]
    private Bullet bullet2Prefab;

    private float timeFired = 0f;
    [SerializeField]
    private float fireDelay = 0.5f;
    [SerializeField]
    private float floorHeight = 0.2f;
    [SerializeField]
    private Position playerPosition;
    [SerializeField]
    private GameObject damageIndicator;
    [SerializeField]
    private BoolFlag levelFinished;
    [SerializeField]
    private BoolFlag inMenu;
    [SerializeField]
    private BoolFlag isDead;


    // Use this for initialization
    void Start()
    {
        crosshair.setParent(this);
        health.CurrentHealth = health.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (inMenu.Value)
        {
            Debug.Log("test");
        }
        else
        {

            float inputY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            if (inputY < 0 && transform.position.y < floorHeight)
            {
                inputY = 0;
            }


            float inputX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            transform.position = new Vector3(transform.position.x + inputX, transform.position.y + inputY, transform.position.z);


            /*float targetY = Input.GetAxis("Vertical2") * Time.deltaTime * crossHairSpeed;
            float targetX = Input.GetAxis("Horizontal2") * Time.deltaTime * crossHairSpeed;
            crosshair.changePos(targetX, targetY);*/

            float targetY = Input.GetAxis("Vertical2") * 0.35f + transform.position.y;
            float targetX = Input.GetAxis("Horizontal2") * 0.35f + transform.position.x;
            crosshair.setPos(targetX, targetY + 0.25f);

            Vector3 bulletStartPos = transform.position + Vector3.back * 2 + Vector3.up * 0.19f;

            if (Input.GetButton("Fire1"))
            {
                if (Time.time - timeFired > fireDelay)
                {
                    Debug.Log("Shoot!");
                    Bullet bullet = Instantiate(bulletPrefab);
                    bullet.transform.position = crosshair.transform.position;
                    bullet.SetDir((bulletStartPos - crosshair.transform.position).normalized);
                    timeFired = Time.time;
                }
            }
            else if (Input.GetButton("Fire2"))
            {
                if (Time.time - timeFired > fireDelay * 2.5f)
                {
                    Debug.Log("Alt shoot!");
                    Bullet bullet = Instantiate(bullet2Prefab);
                    bullet.transform.position = crosshair.transform.position;
                    bullet.SetDir((bulletStartPos - crosshair.transform.position).normalized);
                    timeFired = Time.time;
                }
            }

            playerPosition.Pos = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            health.CurrentHealth -= 2;
            Destroy(other.gameObject);
            //todo death
            StartCoroutine("ShowDamage");
        }
        else if (other.tag == "Obstacle" || other.tag == "EnemyBullet")
        {
            health.CurrentHealth--;
            Destroy(other.gameObject);
            StartCoroutine("ShowDamage");
        }
        else if (other.tag == "Finish")
        {
            levelFinished.Value = true;
            Destroy(other.gameObject);
        }
    }

    IEnumerator ShowDamage()
    {
        damageIndicator.SetActive(true);

        yield return new WaitForSeconds(.3f);

        damageIndicator.SetActive(false);

        yield return new WaitForSeconds(0);
        
        if(health.CurrentHealth <= 0)
        {
            isDead.Value = true;
        }
    }
}
