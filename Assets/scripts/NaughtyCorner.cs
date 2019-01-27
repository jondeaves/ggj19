using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NaughtyCorner : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
