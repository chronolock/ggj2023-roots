using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLeftRight : MonoBehaviour
{

    public bool stoped = false;

    [Range(0, 20)]
    public float moveSpeed = 2;

    public BoxCollider2D moveArea;

    private Rigidbody2D rbody2d;

    private Bounds boundsArea;

    void Start()
    {
        
        rbody2d = GetComponent<Rigidbody2D>();
        boundsArea = moveArea.bounds;
        moveArea.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!stoped)
        {

            if (transform.localScale.x < 0)
            {
                if(transform.position.x < boundsArea.max.x)
                {
                    rbody2d.velocity = new Vector2(moveSpeed * 1, 0);
                } else
                {
                    invertDirection();
                }
            }
            else
            {
                if (transform.position.x > boundsArea.min.x)
                {
                    rbody2d.velocity = new Vector2(moveSpeed * -1, 0);
                }
                else
                {
                    invertDirection();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        checkCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //checkCollision(collision);
    }

    private void checkCollision(Collision2D collision)
    {
        if (rbody2d.velocity.x == 0)
        {
            invertDirection();
        } 
        else
        {
            if (collision.collider.tag == "Player")
            {
                if (collision.gameObject.GetComponent<MainChar>().isDead)
                {
                    invertDirection();
                }
            }
        }
    }

    private void invertDirection()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
