using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    private NavMeshAgent m_Agent;
    private float m_WaitTimer;
    private float m_WaitTime;
    private bool m_IsWaiting;
    private bool m_MakeTheChange;

    // Start is called before the first frame update
    void Start()
    {
        m_IsWaiting = false;
        m_MakeTheChange = false;
        m_Agent = GetComponent<NavMeshAgent>();
        transform.position = RandomNavSphere();
        m_Agent.destination = RandomNavSphere();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_MakeTheChange)
        {
            if (m_WaitTime > 0f)
            {
                m_WaitTimer += Time.deltaTime;

                if (m_WaitTimer > m_WaitTime)
                {
                    m_WaitTimer = 0f;
                    m_WaitTime = 0f;
                }
            }
            else
            {
                m_Agent.destination = RandomNavSphere();
                m_WaitTime = 0f;
            }

            return;
        }

        if (Vector3.Distance(m_Agent.destination, transform.position) <= 3.5f)
        {
            m_WaitTime = Mathf.FloorToInt(Random.Range(0, 6));
            m_IsWaiting = m_WaitTime > 3;
            m_MakeTheChange = true;
        }
    }

    private Vector3 RandomNavSphere()
    {
        Vector3 origin = new Vector3(0f, 0, -0.07f);
        float distance = 20f;
        int layermask = -1;

        Vector3 randomDirection = Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }
}
