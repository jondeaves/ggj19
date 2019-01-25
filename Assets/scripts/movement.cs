using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

	public float speed = 5.0f;
	public Rigidbody rb;
	private bool moving;


	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		//check for movment input
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		//update movement and rotation
		rb.velocity = new Vector3(x * speed, 0, y * speed);

		transform.rotation = Quaternion.LookRotation(rb.velocity);

		moving = true;
		Debug.Log("moving");

		if (moving == true) ;
		{
			speed = speed + Time.deltaTime;
			if (speed > 5.0f)
			{
				Debug.Log(gameObject.transform.position.x + " : " + speed);
			}
		}
		//play movement animation while moving

	}

}

