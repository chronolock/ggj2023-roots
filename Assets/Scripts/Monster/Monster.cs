using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Range(0,10)]
    public int power = 1;

    public bool trapped = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (trapped)
        {
            return;
        }

        if (collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<MainChar>().TakeDamage(transform.position, power);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (trapped)
        {
            return;
        }

        if (collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<MainChar>().TakeDamage(transform.position, power);
        }
    }

    public void TrapMonster()
    {
        trapped = true;

        MovingLeftRight mvLR = gameObject.GetComponent<MovingLeftRight>();
        if (mvLR)
        {
            mvLR.enabled = false;
        }

        RangedAttackAtDistance rangeAtk = gameObject.GetComponent<RangedAttackAtDistance>();
        if (rangeAtk)
        {
            rangeAtk.enabled = false;
            rangeAtk.stopAttack();
        }

        GetComponent<Rigidbody2D>().simulated = false;

        //disableAllColliders();
    }

    public void ReleaseMonster()
    {
        trapped = false;

        MovingLeftRight mvLR = gameObject.GetComponent<MovingLeftRight>();
        if (mvLR)
        {
            mvLR.enabled = true;
        }

        RangedAttackAtDistance rangeAtk = gameObject.GetComponent<RangedAttackAtDistance>();
        if (rangeAtk)
        {
            rangeAtk.enabled = true;
        }

        GetComponent<Rigidbody2D>().simulated = true;
    }

    /*private void disableAllColliders()
    {
        disableCollider<Collider2D>();
        disableCollider<BoxCollider2D>();
        disableCollider<CircleCollider2D>();
        disableCollider<PolygonCollider2D>();
        disableCollider<EdgeCollider2D>();
        disableCollider<CapsuleCollider2D>();
        disableCollider<CompositeCollider2D>();
    }

    private void disableCollider<T> () where T : Collider2D
    {
        T[] colliders = GetComponents<T>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
    }*/
}
