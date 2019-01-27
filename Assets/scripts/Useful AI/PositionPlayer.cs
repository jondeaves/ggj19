using UnityEngine;
using UnityEngine.AI;

public class PositionPlayer : MonoBehaviour
{
    private NavMeshAgent m_Agent;

    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        transform.position = RandomNavSphere();

        switch(GetComponent<MovementV2>().PlayerNumber)
        {
            case 1:
                if (Input.GetJoystickNames().Length == 0)
                {
                    gameObject.SetActive(false);
                }
                break;
            case 2:
                if (Input.GetJoystickNames().Length <= 1)
                {
                    gameObject.SetActive(false);
                }
                break;
            case 3:
                if (Input.GetJoystickNames().Length <= 2)
                {
                    gameObject.SetActive(false);
                }
                break;
            case 4:
                if (Input.GetJoystickNames().Length <= 3)
                {
                    gameObject.SetActive(false);
                }
                break;
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
