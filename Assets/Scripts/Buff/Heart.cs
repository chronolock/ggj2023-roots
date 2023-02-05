using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{

    public AudioClip getLifeAudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(EnvironmentSystem.CurrentLife < EnvironmentSystem.InitLife)
            {
                EnvironmentSystem.CurrentLife++;
            }

            AudioSource audio = gameObject.GetComponent<AudioSource>();

            audio.clip = getLifeAudio;
            audio.Play();

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;

            Destroy(gameObject, 2);
        }
    }
}
