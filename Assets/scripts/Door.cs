using UnityEngine;

public enum OpenDirection
{
    Left,
    Right,
    Up,
    Down
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
    public float m_MoveSpeed = 0.1f;

    private DoorState m_DoorState = DoorState.closed;
    private Vector3 m_StartPosition;
    private Vector3 m_EndPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_StartPosition = gameObject.transform.position;
        m_EndPosition = GetEndPosition();
    }

    // Update is called once per frame
    void Update()
    {
        bool hasHumanInProximity = false;

        // Find nearby 'apache helicopters'
        Collider[] hitColliders = Physics.OverlapSphere(m_StartPosition, 1.4f);

        foreach (Collider col in hitColliders)
        {
            if (col.tag == "thief" || col.tag == "AI")
            {
                hasHumanInProximity = true;
            }
        }


        // IF closed or closing, put back to open/opening if proximity alert
        if (m_DoorState == DoorState.closing && hasHumanInProximity)
        {
            m_DoorState = DoorState.opening;
        }


        // If currently open/closed, then see if we need to trigger a change in state
        if (m_DoorState == DoorState.closed && hasHumanInProximity)
        {
            m_DoorState = DoorState.opening;
        }
        else if (m_DoorState == DoorState.open && !hasHumanInProximity)
        {
            m_DoorState = DoorState.closing;
        }


        // If opening/closing and reaches it's end point then we stop it
        if (IsDoorOpened())
        {
            m_DoorState = DoorState.open;
        }
        else if (IsDoorClosed())
        {
            m_DoorState = DoorState.closed;
        }


        // If we are statically open/closed, then we do not want to do a velocity
        if (m_DoorState == DoorState.closed || m_DoorState == DoorState.open)
        {
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


        gameObject.transform.position += normalizedDirectionVector * m_MoveSpeed;

    }

    private bool IsDoorOpened()
    {
        if (m_DoorState != DoorState.opening)
        {
            return false;
        }


        bool isOpening = false;
        switch (m_OpenDirection)
        {
            case OpenDirection.Left:
                isOpening = gameObject.transform.position.x <= m_EndPosition.x;
                break;
            case OpenDirection.Right:
                isOpening = gameObject.transform.position.x >= m_EndPosition.x;
                break;
        }

        return isOpening;

    }

    private bool IsDoorClosed()
    {
        if (m_DoorState != DoorState.closing)
        {
            return false;
        }


        bool isOpening = false;
        switch (m_OpenDirection)
        {
            case OpenDirection.Left:
                isOpening = gameObject.transform.position.x >= m_StartPosition.x;
                break;
            case OpenDirection.Right:
                isOpening = gameObject.transform.position.x <= m_StartPosition.x;
                break;
        }

        return isOpening;

    }

    private Vector3 GetEndPosition()
    {
        Vector3 endPos = Vector3.zero;

        switch (m_OpenDirection)
        {
            case OpenDirection.Left:
                endPos = gameObject.transform.position - new Vector3(
                    gameObject.GetComponent<BoxCollider>().size.x * gameObject.transform.localScale.x,
                    0,
                    0
                );
                break;
            case OpenDirection.Right:
                endPos = gameObject.transform.position + new Vector3(
                    gameObject.GetComponent<BoxCollider>().size.x * gameObject.transform.localScale.x,
                    0,
                    0
                );
                break;
            case OpenDirection.Up:
                endPos = gameObject.transform.position - new Vector3(
                    0,
                    gameObject.GetComponent<BoxCollider>().size.y * gameObject.transform.localScale.y,
                    0
                );
                break;
            case OpenDirection.Down:
                endPos = gameObject.transform.position + new Vector3(
                    0,
                    gameObject.GetComponent<BoxCollider>().size.y * gameObject.transform.localScale.y,
                    0
                );
                break;
        }


        Debug.Log(m_EndPosition);
        return endPos;
    }
}
