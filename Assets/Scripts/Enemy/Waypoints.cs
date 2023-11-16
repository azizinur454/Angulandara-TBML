using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public float flyingSpeed = 1f;
    public float waypointReachedDistance = 0.1f;
    public List<Transform> waypoints;

    Rigidbody2D rb;
    Transform nextWayPoint;
    int waypointsNum = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        nextWayPoint = waypoints[waypointsNum];
    }

    private void FixedUpdate()
    {
        Flight();
    }

    public void Flight()
    {
        Vector2 directionToWaypoint = (nextWayPoint.position - transform.position).normalized;

        float distance = Vector2.Distance(nextWayPoint.position, transform.position);

        rb.velocity = directionToWaypoint * flyingSpeed;
        flipDirection();

        // kondisi ketika sudah mencapai waypoints tertentu
        if(distance <= waypointReachedDistance)
        {
            // pindah ke waypoint selanjutnya
            waypointsNum++;

            if(waypointsNum >= waypoints.Count)
            {
                // kembali ke waypoint pertama
                waypointsNum = 0;
            }

            nextWayPoint = waypoints[waypointsNum];
        }
    }

    public void flipDirection()
    {
        Vector3 locScale = transform.localScale;

        if(transform.localScale.x > 0)
        {
            // kondisi sprite saat menghadap ke kanan
            if (rb.velocity.x < 0)
            {
                // flip direction sprite
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            // kondisi sprite saat menghadap ke kiri
            if (rb.velocity.x > 0)
            {
                // flip direction sprite
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
    }
}
