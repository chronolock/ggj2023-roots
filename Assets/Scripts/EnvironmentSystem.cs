using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSystem : MonoBehaviour
{

    [Range(1, 10)]
    public int initLife = 3;

    public int currentLife = 3;

    private static EnvironmentSystem instance;

    private void Awake()
    {
        instance = this;
    }


    public static int InitLife
    {
        get{
            return instance.initLife;
        }
    }

    public static int CurrentLife
    {
        get
        {
            return instance.currentLife;
        }
        set
        {
            instance.currentLife = value;
        }
    }
}
