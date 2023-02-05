using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularTrap : MonoBehaviour
{

    [Range(0, 360)]
    public float rotateSpeed = 1;

    public int power = 1;

    void Update()
    {
        transform.Rotate(0, 0, -1 * rotateSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<MainChar>().TakeDamage(transform.position, power);
        }
    }
}
