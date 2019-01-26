using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public ScapFood happyDoggo;

    private const float INPUT_DEADZONE = 0.19f;

    [SerializeField]
    [Tooltip("Base movement speed")]
    public float Speed = 150.0f;

    [SerializeField]
    [Tooltip("Which gamepad player will be bound to")]
    public int PlayerNumber = 1;

    private Rigidbody m_RigidBody;
	private bool m_IsMoving;

	// Start is called before the first frame update
	void Start()
	{
		GameObject dogoate = GameObject.FindGameObjectWithTag ("dog");

		m_RigidBody = GetComponent<Rigidbody>();
        m_IsMoving = false;
    }

	// Update is called once per frame
	void Update()
	{
		//check if dog ate
		if (happyDoggo != null && happyDoggo.justAte == true)
		{
			Speed *= 1.5f;
		}

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
            (Input.GetAxis("Horizontal " + PlayerNumber) * Speed) * Time.deltaTime, 
            0,
            (Input.GetAxis("Vertical " + PlayerNumber) * Speed) * Time.deltaTime
        );

        transform.rotation = Quaternion.LookRotation(m_RigidBody.velocity);

    }

}

