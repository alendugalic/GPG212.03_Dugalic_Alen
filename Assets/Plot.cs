using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
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
        Debug.Log("Build tower here: " + name);
        if (tower != null) return;

       TowerMain towerToBuild = BuildManager.Instance.GetSelectedTower();

        if (towerToBuild.cost > LevelManager.instance.gold)
        {
            return;
        }
        LevelManager.instance.SpendGold(towerToBuild.cost);

        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
    }
    
}
