using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private const float INPUT_DEADZONE = 0.19f;

	public float speed = 150.0f;
	private Rigidbody m_RigidBody;
	private bool m_IsMoving;
    public int PlayerNumber = 1;

	// Start is called before the first frame update
	void Start()
	{
		m_RigidBody = GetComponent<Rigidbody>();
        m_IsMoving = false;
    }

	// Update is called once per frame
	void Update()
	{
        m_IsMoving = Input.GetAxis("Horizontal " + PlayerNumber) > INPUT_DEADZONE || Input.GetAxis("Horizontal " + PlayerNumber) < -INPUT_DEADZONE ||
            Input.GetAxis("Vertical " + PlayerNumber) > INPUT_DEADZONE || Input.GetAxis("Vertical " + PlayerNumber) < -INPUT_DEADZONE;

        if (!m_IsMoving)
        {
            m_RigidBody.velocity = Vector3.zero;
            transform.rotation = new Quaternion();
            return;
        }

        //update movement and rotation if we are not within our deadzone
        m_RigidBody.velocity = new Vector3(
            (Input.GetAxis("Horizontal " + PlayerNumber) * speed) * Time.deltaTime, 
            0,
            (Input.GetAxis("Vertical " + PlayerNumber) * speed) * Time.deltaTime
        );

        transform.rotation = Quaternion.LookRotation(m_RigidBody.velocity);

    }

}

