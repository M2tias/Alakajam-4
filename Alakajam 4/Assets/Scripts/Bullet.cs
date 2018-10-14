using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 dir;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lifetime = 5f;
    [SerializeField]
    private BulletType bulletType;
    private float startTime = 0f;

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x - dir.x * speed * Time.deltaTime;
        float y = transform.position.y - dir.y * speed * Time.deltaTime;
        float z = transform.position.z - dir.z * speed * Time.deltaTime;
        transform.position = new Vector3(x, y, z);

        if(Time.time - startTime > lifetime)
        {
            Destroy(gameObject);
        }
    }

    public void SetDir(Vector3 dir)
    {
        this.dir = dir;
    }

    public void SetType(BulletType type)
    {
        bulletType = type;
    }

    public void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Enemy" && bulletType == BulletType.Banish) ||
           (other.tag == "Obstacle" && bulletType == BulletType.Destroy))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}

public enum BulletType
{
    Destroy,
    Banish,
    Enemy
}