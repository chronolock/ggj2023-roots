using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Range(0,10)]
    public int power = 1;

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
        if (collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<MainChar>().TakeDamage(transform.position, power);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<MainChar>().TakeDamage(transform.position, power);
        }
    }

    public void TrapMonster()
    {
        MovingLeftRight mvLR = gameObject.GetComponent<MovingLeftRight>();
        if (mvLR)
        {
            mvLR.enabled = false;
        }

        GetComponent<Rigidbody2D>().simulated = false;
    }
}
