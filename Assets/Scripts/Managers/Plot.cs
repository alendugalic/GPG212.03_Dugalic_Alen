using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    public GameObject towerObject;
    public Tower tower;
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
        if (UIManager.Instance.IsHoveringUI()) return;
     
        if (towerObject != null)
        {
            tower.OpenUpgradeUI();
            return;
        }

       TowerMain towerToBuild = BuildManager.Instance.GetSelectedTower();

        if (towerToBuild.cost > LevelManager.instance.gold)
        {
            return;
        }
        LevelManager.instance.SpendGold(towerToBuild.cost);

        towerObject = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        tower = towerObject.GetComponent<Tower>();
    }
    
}
