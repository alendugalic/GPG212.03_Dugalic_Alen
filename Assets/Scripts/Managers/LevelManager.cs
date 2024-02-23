using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform[] path;
    public Transform startPoint;

    private void Awake()
    {
        instance = this; 
    }
}
