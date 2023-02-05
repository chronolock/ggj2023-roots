using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LinerBullet : EnemyBullet
{

    public float speed = 2;
    private Vector2 direction = Vector2.zero;
    private Rigidbody2D rbody2D;

    
    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hurled)
        {
            rbody2D.velocity = direction * speed;
        }
        else
        {
            rbody2D.velocity = Vector2.zero;
        }
    }

    public override void throwBullet()
    {
        GameObject playerRef = GameObject.FindGameObjectWithTag("Player");
        direction = new Vector2(playerRef.transform.position.x - transform.position.x > 0 ? 1 : -1, 0);
        hurled = true;
    }
}
