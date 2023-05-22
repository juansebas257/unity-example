using UnityEngine;

public class WaypointsFollowUp : MonoBehaviour {

    public GameObject[] waypoints;
    public float speed = 2f;
    private int currentWaypointIndex = 0;

    void Update() {
        if(Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f) {
            currentWaypointIndex ++;
            if(currentWaypointIndex >= waypoints.Length) {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}
