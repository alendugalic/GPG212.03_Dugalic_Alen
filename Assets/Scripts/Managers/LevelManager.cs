using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform[] path;
    public Transform startPoint;

    public int gold;

    private void Awake()
    {
        instance = this; 
    }
    private void Start()
    {
        gold = 50;
    }

    public void IncreaseGold(int amount)
    {
        gold += amount;
    }
    public bool SpendGold(int amount)
    {
        if(amount <= gold)
        {
            gold -= amount;
            return true;
        }
        else
        {
            Debug.Log("You dont have enough");
            return false;
        }
    }
}
