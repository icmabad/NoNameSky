using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winrocket : MonoBehaviour
{
    private GameObject waypoints;
    private const float kSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        waypoints = GameObject.Find("waypoint");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints.transform.position, Time.deltaTime * kSpeed);
    }
}
