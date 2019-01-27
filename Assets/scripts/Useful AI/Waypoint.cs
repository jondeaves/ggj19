using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint[] m_Neighbors;


    // Start is called before the first frame update
    void Start()
    {
        m_Neighbors = new Waypoint[] { };
    }

    // Update is called once per frame
    //OnDrawGizmosSelected
    void OnDrawGizmos()
    {
        if (m_Neighbors == null)
        {
            return;
        }

        Gizmos.color = Color.green;

        foreach (Waypoint neighbor in m_Neighbors)
        {
            if (neighbor != null)
            {
                Handles.Label(transform.position, name);
                Gizmos.DrawSphere(transform.position, 0.1f);
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
            }
        }

    }
}
