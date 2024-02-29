using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    public GameObject towerObject;
   
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }
    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    void OnMouseExit()
    {
        sr.color = startColor;  
    }
    private void OnMouseDown()
    {
        

        if (towerObject != null)
        {
            UpgradeTower();

        }
        else
        {
            BuildNewTower();
        }
    }

    private void UpgradeTower()
    {
        TowerMain towerToBuild = BuildManager.Instance.GetSelectedTower();

        if (towerToBuild.cost > LevelManager.instance.gold)
        {
            // Not enough gold to upgrade.
            return;
        }

        LevelManager.instance.SpendGold(towerToBuild.cost);

        // Destroy the existing tower and instantiate the upgraded one.
        Destroy(towerObject);
        towerObject = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
       
    }

    private void BuildNewTower()
    {
        TowerMain towerToBuild = BuildManager.Instance.GetSelectedTower();

        if (towerToBuild.cost > LevelManager.instance.gold)
        {
            // Not enough gold to build.
            return;
        }

        LevelManager.instance.SpendGold(towerToBuild.cost);

        // Instantiate the new tower.
        towerObject = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
       
    }

}
