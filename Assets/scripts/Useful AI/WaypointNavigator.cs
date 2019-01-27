using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Base movement speed")]
    public float m_Speed = 0.1f;

    [SerializeField]
    [Tooltip("Base turn speed")]
    public float m_TurnSpeed = 5f;

    private GameObject m_NextWaypoint;
    private GameObject m_CurrentWaypoint;

    private System.Random m_Random;


    // Start is called before the first frame update
    void Start()
    {
        m_Random = new System.Random();
        m_CurrentWaypoint = FindClosestWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        //if (m_CurrentWaypoint == null)
        //{
        //    // Find new waypoint to go to off the bat
        //    FindNewWaypoint();
        //}
        //else if (m_NextWaypoint == null)
        //{
        //    // If we have a current waypoint target, find the next one
        //    FindNextWaypoint(m_CurrentWaypoint);
        //}

        if (m_CurrentWaypoint != null)
        {
            NavigateToWaypoint();
        }
    }

    private void NavigateToWaypoint()
    {
        Vector3 forwardVector = m_CurrentWaypoint.transform.position - transform.position;
        forwardVector.Normalize();

        transform.position += (forwardVector * m_Speed);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(forwardVector), Time.time * m_TurnSpeed);

        if (Vector3.Distance(transform.position, m_CurrentWaypoint.transform.position) <= 1f)
        {
            //m_NextWaypoint = FindConnectedWaypoint();
            m_CurrentWaypoint = FindClosestWaypoint();
            //if (m_NextWaypoint != null)
            //{ 
            //    //m_CurrentWaypoint = m_NextWaypoint;
            //    FindNextWaypoint();
            //}
            //else
            //{
            //    //m_CurrentWaypoint = null;
            //}
            //Debug.Log(m_CurrentWaypoint);
            //Debug.Log(m_NextWaypoint);
            //Debug.Log("---------------");
            //m_CurrentWaypoint = m_NextWaypoint;
            //FindNextWaypoint(m_CurrentWaypoint);
            //Debug.Log(m_CurrentWaypoint);
            //Debug.Log(m_NextWaypoint);
            //Debug.Log("\n\n\n\n");
        }
    }

    private GameObject FindClosestWaypoint()
    {
        GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        GameObject closestWaypoint = null;

        foreach (GameObject wpObj in allWaypoints)
        {
            if (closestWaypoint == null && (m_CurrentWaypoint == null || wpObj.name != m_CurrentWaypoint.name))
            {
                closestWaypoint = wpObj;
            }
            else if (m_CurrentWaypoint == null || wpObj.name != m_CurrentWaypoint.name)
            {
                float distanceFromCurrentWaypoint = Vector3.Distance(wpObj.transform.position, transform.position);
                float distanceFromClosestSoFar = Vector3.Distance(closestWaypoint.transform.position, transform.position);

                if (distanceFromCurrentWaypoint < distanceFromClosestSoFar)
                {
                    closestWaypoint = wpObj;
                }
            }
        }

        return closestWaypoint;
    }

    private GameObject FindConnectedWaypoint()
    {
        Waypoint[] connectedWaypoints = m_CurrentWaypoint.GetComponent<Waypoint>().m_Neighbors;

        Debug.Log(connectedWaypoints.Length);

        //Debug.Log(m_CurrentWaypoint);
        //Debug.Log(m_CurrentWaypoint.name);
        //Debug.Log(connectedWaypoints.Length);

        return null;

        //Waypoint closestWaypoint = null;

        //foreach (GameObject wpObj in allWaypoints)
        //{
        //    Waypoint wp = wpObj.GetComponent<Waypoint>();

        //    if (wp != null)
        //    {
        //        if (closestWaypoint == null && (m_CurrentWaypoint == null || wp.name != m_CurrentWaypoint.name))
        //        {
        //            closestWaypoint = wp;
        //        }
        //        else if (m_CurrentWaypoint == null || wp.name != m_CurrentWaypoint.name)
        //        {
        //            float distanceFromCurrentWaypoint = Vector3.Distance(wp.transform.position, transform.position);
        //            float distanceFromClosestSoFar = Vector3.Distance(closestWaypoint.transform.position, transform.position);

        //            if (distanceFromCurrentWaypoint < distanceFromClosestSoFar)
        //            {
        //                closestWaypoint = wp;
        //            }
        //        }
        //    }
        //}

        //return closestWaypoint;
    }

    //private Waypoint FindNextWaypoint()
    //{
    //    m_NextWaypoint = null;

    //    //Debug.Log("Looking for next waypoint after: " + m_CurrentWaypoint.name + " which has: " + m_CurrentWaypoint.m_Neighbors.Length + " neighbors");

    //    if (m_CurrentWaypoint != null && m_CurrentWaypoint.m_Neighbors.Length > 0)
    //    {
    //        m_NextWaypoint = m_CurrentWaypoint.m_Neighbors[
    //            m_Random.Next(0, m_CurrentWaypoint.m_Neighbors.Length - 1)
    //        ];
    //    }

    //    Debug.Log("Found next waypoint: " + m_NextWaypoint.name);
    //    return m_NextWaypoint;
    //}
}