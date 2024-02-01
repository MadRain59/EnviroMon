using System.Collections;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;
    [SerializeField] private float pauseDuration = 1f;

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
