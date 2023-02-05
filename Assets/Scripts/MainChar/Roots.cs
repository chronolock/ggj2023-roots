using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roots : MonoBehaviour
{
    private float timeCount = 0;

    private float timeToDestroy = 0;

    private Monster monsterToRelease;

    void Update()
    {
        if(timeToDestroy == 0)
        {
            return;
        }

        timeCount += Time.deltaTime;

        if(timeCount >= timeToDestroy)
        {
            if(monsterToRelease != null)
            {
                monsterToRelease.ReleaseMonster();
            }
            Destroy(gameObject);
        }
    }

    public void DestroyAt(float time)
    {
        timeToDestroy = time;
    }

    public void DestroyAt(float time, Monster monster)
    {
        monsterToRelease = monster;
        DestroyAt(time);
    }

}
