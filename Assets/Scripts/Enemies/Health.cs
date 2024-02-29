using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float maxHP = 2;
    [SerializeField] private int goldValue = 5;

    [Header("UI")]
    public Slider healthSlider; 

    private float currentHP;
    private bool isDestroyed = false;

    private void Start()
    {
        currentHP = maxHP;
        healthSlider.value = currentHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        UpdateHealthSlider(currentHP, maxHP);

        if (currentHP <= 0 && !isDestroyed)
        {
            Spawner.onEnemyDestroy.Invoke();
            LevelManager.instance.IncreaseGold(goldValue);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

    private void UpdateHealthSlider(float currentHP, float HP)
    {
        healthSlider.value = currentHP / HP;
    }
}
