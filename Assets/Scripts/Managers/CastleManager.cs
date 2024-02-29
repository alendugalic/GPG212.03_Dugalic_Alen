using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleManager : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private int upgradeCost = 10;

    [SerializeField] private GameObject[] castleTiers;
    //public GameObject tier1Canvas;
    //public Canvas tier2Canvas;
    //public Canvas tier3Canvas;
    //public Canvas tier4Canvas;

    private int castleTier = 0;
   
    private int upgradeMultiplier = 2;  

    void Start()
    {
        UpdateTiers();
        upgradeButton.onClick.AddListener(UpgradeCastle);
    }

    void UpgradeCastle()
    {
      
        if (LevelManager.instance.SpendGold(upgradeCost))
        { 
            castleTier++;    
            UpdateTiers();
            upgradeCost *= upgradeMultiplier;
        }
    }

    void UpdateTiers()
    {
        // Disable all castle tiers and their children recursively
        for (int i = 0; i < castleTiers.Length; i++)
        {
            SetActiveRecursively(castleTiers[i], false);
        }

        // Enable the castle tier and its children recursively based on the index
        if (castleTier >= 0 && castleTier < castleTiers.Length && castleTiers[castleTier] != null)
        {
            SetActiveRecursively(castleTiers[castleTier], true);
        }
    }

    void SetActiveRecursively(GameObject obj, bool value)
    {
        obj.SetActive(value);

        // Recursively set the active state for all children
        foreach (Transform child in obj.transform)
        {
            SetActiveRecursively(child.gameObject, value);
        }
    }
}

