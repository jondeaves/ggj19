using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OpenDirection
{
    Left,
    Right
}

public enum DoorState
{
    open,
    opening,
    closed,
    closing
}

public class Door : MonoBehaviour
{
    public OpenDirection m_OpenDirection = OpenDirection.Left;
    public float m_MoveSpeed = 5f;

    private DoorState m_DoorState = DoorState.closed;
    private Vector3 m_StartPosition;
    private Vector3 m_EndPosition;
    private Rigidbody m_RigidBody;

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_StartPosition = gameObject.transform.position;
        m_EndPosition = gameObject.transform.position - new Vector3(
            gameObject.GetComponent<BoxCollider>().size.x * gameObject.transform.localScale.x,
            0,
            0
        );
    }

    // Update is called once per frame
    void Update()
    {
        bool hasHumanInProximity = false;

        // Find nearby 'apache helicopters'
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f);

        foreach (Collider col in hitColliders)
        {
            if (col.tag == "thief" || col.tag == "AI")
            {
                hasHumanInProximity = true;
            }
        }


        // If currently open/closed, then see if we need to trigger a change in state
        if (m_DoorState == DoorState.closed && hasHumanInProximity)
        {
            m_DoorState = DoorState.opening;
        }
        else if (m_DoorState == DoorState.open && !hasHumanInProximity)
        {
            Debug.Log("Closing door");
            m_DoorState = DoorState.closing;
        }


        // If opening/closing and reaches it's end point then we stop it
        if (m_DoorState == DoorState.opening)
        {
            if (gameObject.transform.position.x <= m_EndPosition.x)
            {
                m_DoorState = DoorState.open;
            }
        }
        else if (m_DoorState == DoorState.closing)
        {
            if (gameObject.transform.position.x >= m_StartPosition.x)
            {
                m_DoorState = DoorState.closed;
            }
        }


        // If we are statically open/closed, then we do not want to do a velocity
        if (m_DoorState == DoorState.closed || m_DoorState == DoorState.open)
        {
            Debug.Log("We are where we need to be");
            m_RigidBody.velocity = Vector3.zero;
            return;
        }


        // If we are opening/closing then calculat eour velocity
        Vector3 normalizedDirectionVector = Vector3.zero;

        if (m_DoorState == DoorState.opening)
        {
            normalizedDirectionVector = Vector3.Normalize(m_EndPosition - m_StartPosition);
        }
        else if (m_DoorState == DoorState.closing)
        {
            normalizedDirectionVector = Vector3.Normalize(m_StartPosition - m_EndPosition);
        }


        //update movement and rotation if we are not within our deadzone
        m_RigidBody.velocity = normalizedDirectionVector;

        Debug.Log(normalizedDirectionVector);
        
    }
}
