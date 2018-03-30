﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuardMovement : MonoBehaviour {

    //waypoints for guard to follow
    public Transform[] waypoints;
    //current waypoint they are seeking
    private int currWaypointNum;
    //movement speed
    public float speed;
    //vision cone
    private GameObject vision;
    //player
    private GameObject player;
    //check if guard is distracted
    public bool distracted = false;
    // Use this for initialization
    void Start () {
        //start on waypoint 0
        currWaypointNum = 0;

        //get child vision object
        vision = this.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!distracted)
        {
            SeekPoint(waypoints[currWaypointNum]);
        }
	}

    //moves guard towards current waypoint, updates current waypoint
   private void SeekPoint(Transform waypointLoc)
    {
        //move towards point
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, waypointLoc.position, step);

        //turn to face point, movement is somewhat unnatural, may need to be edited later
        var targetPoint = waypointLoc.transform.position;
        var targetRotation = Quaternion.LookRotation(targetPoint - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);

        //calculate distance to waypoint
        float dist = Vector3.Distance(this.transform.position, waypointLoc.transform.position);

        //if it reaches the location, cycle to the next waypoint
        if (dist < 1)
        {
            currWaypointNum += 1;
            if(currWaypointNum >= waypoints.Length)
            {
                currWaypointNum = 0;
            }
        }
    }

    

}