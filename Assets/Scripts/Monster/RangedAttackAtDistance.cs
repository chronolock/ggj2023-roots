using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackAtDistance : MonoBehaviour
{

    [Range(0, 30)]
    public float distance = 4;

    [Range(0, 10)]
    public float cooldown = 2;

    [Range(0, 10)]
    public float castTime = 1;

    public bool eyesOnPlayer = true;

    public bool stopAttackWhenPlayersOut = false;

    public EnemyBullet bullet;

    public Transform bulletStartPos;

    private bool isVisible = false;

    private GameObject playerRef;

    private EnemyBullet currentBullet;

    private float cooldownCount = 0;
    private float castCount = 0;

    private bool castingBullet = false;
    private bool inCooldown = false;

    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isVisible)
        {
            if (eyesOnPlayer)
            {
                if(playerRef.transform.position.x > transform.position.x)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
                } else
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
            }

            if(Vector2.Distance(playerRef.transform.position, transform.position) < distance)
            {
                startAttack();
            } else
            {
                if (stopAttackWhenPlayersOut)
                {
                    stopAttack();
                }
            }

            if (castingBullet)
            {
                castCount += Time.deltaTime;
                if(castCount > castTime)
                {
                    throwBullet();
                }

            }

            if (inCooldown)
            {
                cooldownCount += Time.deltaTime;
                if(cooldownCount >= cooldown)
                {
                    inCooldown = false;
                }
            }
        }
    }

    private void OnBecameVisible()
    {
        isVisible = true;
    }

    private void OnBecameInvisible()
    {
        isVisible = false;
    }

    public void startAttack(bool forceAttack)
    {
        if (forceAttack || (!inCooldown && !castingBullet))
        {
            currentBullet = Instantiate(bullet);
            currentBullet.transform.position = bulletStartPos.position;
            castingBullet = true;
            castCount = 0;
        }
    }

    public void startAttack()
    {
        startAttack(false);
    }

    public void throwBullet()
    {
        currentBullet.throwBullet();
        castingBullet = false;
        inCooldown = true;
        cooldownCount = 0;
    }

    public void stopAttack()
    {
        if (castingBullet)
        {
            Destroy(currentBullet.gameObject);
        }
    }
}
