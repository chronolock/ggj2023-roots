using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeds : MonoBehaviour
{

    public static int SeedsInStage = 0;

    public GameObject roots;

    [Range(0, 360)]
    public float rotateSpeed = 1;

    [Range(0, 50)]
    public float moveSpeed = 1;

    [Range(0, 50)]
    public float timeLimit = 10;

    private Vector2 move = Vector2.zero;

    private Rigidbody2D rbody2D;

    private bool stopSeed = false;
    private float lifeTimeCount = 0;

    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
        SeedsInStage++;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, stopSeed ? 0 : -1 * rotateSpeed);
        lifeTimeCount += Time.deltaTime;

        if(lifeTimeCount > timeLimit)
        {
            DestroySeed();
        }
    }

    private void FixedUpdate()
    {
        if(move != Vector2.zero && !stopSeed)
        {
            rbody2D.velocity = move * moveSpeed;
        } else
        {
            rbody2D.velocity = Vector2.zero;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.tag != "Player" && collision.tag != "Seed")
            {
                stopSeed = true;

                if (collision.tag == "Enemy")
                {
                    BuildPrisonRoots(collision.transform.position, collision.GetComponent<Monster>());
                }
                else
                {

                    if (EnvironmentSystem.RootsOverRoots)
                    {
                        BuildRoots(transform.position);
                    } else
                    {
                        if(collision.tag != "Roots")
                        {
                            BuildRoots(transform.position);
                        }
                    }
                }

                DestroySeed();
            }           
        }
    }

    private void BuildRoots(Vector2 position)
    {
        GameObject tmpRoots = Instantiate(roots);
        tmpRoots.transform.position = position;
        tmpRoots.AddComponent<Roots>().DestroyAt(EnvironmentSystem.RootPrisonDuration);
    }

    private void BuildPrisonRoots(Vector2 position, Monster monster)
    {
        monster.TrapMonster();
        GameObject tmpRoots = Instantiate(roots);
        tmpRoots.transform.position = position;
        tmpRoots.AddComponent<Roots>().DestroyAt(EnvironmentSystem.RootPrisonDuration, monster);
    }


    public void DestroySeed()
    {
        SeedsInStage--;
        Destroy(gameObject);
    }

    public void throwSeeds(Vector2 angle)
    {
        move = angle;
    }
}
