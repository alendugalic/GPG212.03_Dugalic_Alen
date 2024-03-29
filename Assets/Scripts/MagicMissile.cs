using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissile : MonoBehaviour
{
    private Transform target;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float missileSpeed = 5f;
    [SerializeField] private int missileDamage = 1;
    public void SetTarget(Transform _target)
    {
        target = _target;
    }
    private void Update()
    {
        Destroy(gameObject, 1);
    }
    private void FixedUpdate()
    {
        if(!target) return;

        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * missileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy")) 
        {
            Health enemyHealth = collision.gameObject.GetComponent<Health>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(missileDamage);
            }

            Destroy(gameObject);
        }
    }
}
