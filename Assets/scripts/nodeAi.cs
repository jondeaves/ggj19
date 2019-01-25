using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeAi : MonoBehaviour {

	public GameObject[] node;
	public int maxNodes;

	public float speed = 5.0f;
	public Rigidbody rb;
	private bool moving;

	private float distanceToGo;
	public int objectToGoTo;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckClosest ();
		if (node[objectToGoTo].transform.position == this.transform.position) 
		{
			objectToGoTo = Random.Range (0, node.Length);
		}

		// Move our position a step closer to the target.
		float step =  speed * Time.deltaTime; // calculate distance to move
		transform.position = Vector3.MoveTowards(transform.position, node[objectToGoTo].transform.position, step);

		//update movement and rotation
		//rb.velocity = new Vector3(x * speed, 0, y * speed);

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

	void CheckClosest ()
	{ 
		
		for (int i = 0; i < maxNodes; i++) 
		{
		float distance = Vector3.Distance (this.transform.position, node[i].transform.position);

			if (distance < distanceToGo) 
			{
				distance = distanceToGo;
				objectToGoTo = i;
			}
		}
	}
}
