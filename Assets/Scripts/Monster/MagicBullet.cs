using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MagicBullet : EnemyBullet
{
    public float speed = 2;
    private Vector2 direction = Vector2.zero;
    private Rigidbody2D rbody2D;

    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
    }

    public override void throwBullet()
    {
        GameObject playerRef = GameObject.FindGameObjectWithTag("Player");
        direction = (playerRef.transform.position - transform.position).normalized;
        hurled = true;
    }

    void FixedUpdate()
    {
        if(hurled)
        {
            rbody2D.velocity = direction * speed;
        } else
        {
            rbody2D.velocity = Vector2.zero;
        }
    }
}
