using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    public float movementSpeed = 3f;

    
    void Update()
    {
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < 0.1f)
        {
            currentWaypointIndex++;
            if ( currentWaypointIndex >= waypoints.Length )
            {
                currentWaypointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, movementSpeed * Time.deltaTime);
    }
}
