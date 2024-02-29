using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private Transform target;
    private int remainingBounces = 10;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float missileSpeed = 5f;
    [SerializeField] private int missileDamage = 1;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * missileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Health>().TakeDamage(missileDamage);

        remainingBounces--;

        if (remainingBounces > 0)
        {
            ReflectShuriken();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void ReflectShuriken()
    {
        rb.velocity = Vector2.Reflect(rb.velocity, transform.right);
    }
}
