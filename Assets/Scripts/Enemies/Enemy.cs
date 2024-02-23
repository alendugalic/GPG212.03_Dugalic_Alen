using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes Enemy")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target; // end point
    private int pathIndex = 0;
    private void Start()
    {
        target = LevelManager.instance.path[pathIndex];
        transform.position = target.transform.position;
    }
    private void Update()
    {
        Move();
        
    }
    private void Move()
    {
        // if enemy didnt reach last waypoint it can move
        // if enemy reached last waypoint then it stops
        if (pathIndex <= LevelManager.instance.path.Length - 1)
        {

            transform.position = Vector2.MoveTowards(transform.position, LevelManager.instance.path[pathIndex].transform.position, moveSpeed * Time.deltaTime);

            //if enemy reaches position of waypoint he moves towards
            //then the waypointindex is increased by 1
            //and enemy starts to walk to next 1
            if (transform.position == LevelManager.instance.path[pathIndex].transform.position)
            {
                pathIndex += 1;
            }

        }
        if (pathIndex < LevelManager.instance.path.Length)
        {
            Vector2 targetPosition = LevelManager.instance.path[pathIndex].position;
            Vector2 moveDirection = (targetPosition - (Vector2)transform.position).normalized;
            Vector2 newPosition = (Vector2)transform.position + moveDirection * moveSpeed * Time.deltaTime;

            // Move the Rigidbody2D to the new position
            GetComponent<Rigidbody2D>().MovePosition(newPosition);

            // Calculate the angle of rotation based on the moveDirection
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90;

            // Create a Quaternion rotation from the angle
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            // Apply the rotation to the object
            transform.rotation = rotation;

            // Check if the object is close to the waypoint, then change the waypointIndex
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                pathIndex++;

                if (pathIndex == LevelManager.instance.path.Length)
                {
                    Spawner.onEnemyDestroy.Invoke();
                    Destroy(gameObject);
                    return; //getting to the last point
                }
            }


        }
    }

}
