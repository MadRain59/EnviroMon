using System.Collections;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public GameObject[] waypoints;
    public int currentWaypointIndex = 0;

    public float speed = 2f;
    
    public float pauseDuration = 1f;

    private bool isPaused = false;

    private void Update()
    {
        if (!isPaused)
        {
            float distanceToWaypoint = Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position);

            if (distanceToWaypoint < 0.1f)
            {
                StartCoroutine(PauseForDuration());
                currentWaypointIndex++;

                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
            }

            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        }
    }

    private IEnumerator PauseForDuration()
    {
        isPaused = true;
        yield return new WaitForSeconds(pauseDuration);
        isPaused = false;
    }
}
