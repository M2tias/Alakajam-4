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
    [SerializeField]
    private AudioSource shootSound;
    [SerializeField]
    private AudioSource hurtSound;
    private float mouseX;
    private float mouseY;
    private float lastMouse = 0f;
    private float waitMouse = 1f;
    float targetY = 0;
    float targetX = 0;

    // Use this for initialization
    void Start()
    {
        crosshair.setParent(this);
        health.CurrentHealth = health.MaxHealth;
        shootSound = GetComponent<AudioSource>();
        mouseX = 0;
        mouseY = 0;
        targetY = 0;
        targetX = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (inMenu.Value)
        {
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
            if (Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.05f || Mathf.Abs(Input.GetAxis("Mouse X")) > 0.05)
            {
                mouseY += Input.GetAxis("Mouse Y") * 0.05f;
                mouseX += Input.GetAxis("Mouse X") * 0.05f;
                if (Mathf.Abs(mouseY) > 1f) mouseY = Mathf.Sign(mouseY) * 1f;
                if (Mathf.Abs(mouseX) > 1f) mouseX = Mathf.Sign(mouseX) * 1f;
                Vector3 newTarget = transform.position - new Vector3(mouseX, mouseY, 0);
                targetX = newTarget.x;
                targetY = newTarget.y;
                lastMouse = Time.time;
            }
            else if(Time.time - lastMouse > waitMouse)
            {
                targetY = Input.GetAxis("Vertical2") * 0.35f + transform.position.y;
                targetX = Input.GetAxis("Horizontal2") * 0.35f + transform.position.x;
            }

            crosshair.setPos(targetX, targetY + 0.25f);

            Vector3 bulletStartPos = transform.position + Vector3.back * 2 + Vector3.up * 0.19f;

            if (Input.GetButton("Fire1"))
            {
                if (Time.time - timeFired > fireDelay)
                {
                    shootSound.pitch = 1f;
                    Bullet bullet = Instantiate(bulletPrefab);
                    bullet.transform.position = crosshair.transform.position;
                    bullet.SetDir((bulletStartPos - crosshair.transform.position).normalized);
                    timeFired = Time.time;
                    shootSound.Play();
                }
            }
            else if (Input.GetButton("Fire2"))
            {
                if (Time.time - timeFired > fireDelay * 2.5f)
                {
                    Bullet bullet = Instantiate(bullet2Prefab);
                    bullet.transform.position = crosshair.transform.position;
                    bullet.SetDir((bulletStartPos - crosshair.transform.position).normalized);
                    timeFired = Time.time;
                    shootSound.pitch = 0.5f;
                    shootSound.Play();
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
            hurtSound.Play();
        }
        else if (other.tag == "Obstacle" || other.tag == "EnemyBullet")
        {
            health.CurrentHealth--;
            Destroy(other.gameObject);
            StartCoroutine("ShowDamage");
            hurtSound.Play();
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
