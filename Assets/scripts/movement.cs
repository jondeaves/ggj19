using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private const float INPUT_DEADZONE = 0.19f;

	public float speed = 200.0f;
	public Rigidbody rb;
	private bool m_IsMoving;
    public int playerNumber = 1;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
        m_IsMoving = false;
    }

	// Update is called once per frame
	void Update()
	{
        m_IsMoving = Input.GetAxis("Horizontal " + playerNumber) > INPUT_DEADZONE || Input.GetAxis("Horizontal " + playerNumber) < -INPUT_DEADZONE ||
            Input.GetAxis("Vertical " + playerNumber) > INPUT_DEADZONE || Input.GetAxis("Vertical " + playerNumber) < -INPUT_DEADZONE;

        if (!m_IsMoving)
        {
            rb.velocity = Vector3.zero;
            transform.rotation = new Quaternion();
            return;
        }

        // check for movment input
        float x = (Input.GetAxis("Horizontal " + playerNumber) * speed) * Time.deltaTime;
		float y = (Input.GetAxis("Vertical " + playerNumber) * speed) * Time.deltaTime;

        //update movement and rotation if we are not within our deadzone
        rb.velocity = new Vector3(x, 0, y);

        transform.rotation = Quaternion.LookRotation(rb.velocity);

    }

}

