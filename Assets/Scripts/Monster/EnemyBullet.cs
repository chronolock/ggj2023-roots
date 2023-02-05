using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBullet : MonoBehaviour
{
    [Range(1, 10)]
    public int power = 1;

    protected bool hurled = false;

    public abstract void throwBullet();

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hurled)
        {
            return;
        }

        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<MainChar>().TakeDamage(transform.position, power);
            Destroy(gameObject);
        } else
        {
            if(collision.tag != "Enemy" && collision.tag != "EnemyBullet")
            {
                Destroy(gameObject);
            }
        }
    }

}



