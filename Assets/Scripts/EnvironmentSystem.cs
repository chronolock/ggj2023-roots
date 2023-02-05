using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSystem : MonoBehaviour
{

    [Range(1, 10)]
    public int initLife = 3;

    [Range(1, 10)]
    public int currentLife = 3;

    [Range(0, 10)]
    public float rootDuration = 5;

    [Range(0, 10)]
    public float rootPrisonDuration = 5;

    public bool rootsOverRoots = true;

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

    public static float RootDuration
    {
        get
        {
            return instance.rootDuration;
        }
    }

    public static float RootPrisonDuration
    {
        get
        {
            return instance.rootPrisonDuration;
        }
    }

    public static bool RootsOverRoots
    {
        get
        {
            return instance.rootsOverRoots;
        }
    }
}
