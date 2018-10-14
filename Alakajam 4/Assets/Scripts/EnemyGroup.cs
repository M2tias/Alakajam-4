using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [SerializeField]
    List<Enemy> enemies = new List<Enemy>();
    [SerializeField]
    private bool allCanShoot = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (allCanShoot)
        {
            enemies.ForEach(x => x.CanShoot(true));
        }
        else
        {
            for (var i = 1; i < enemies.Count; i++)
            {
                enemies[i].CanShoot(false);
            }
        }
    }
}
