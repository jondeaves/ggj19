using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeAiV2 : MonoBehaviour {

	public GameObject[] nodeArray;
	public float DistanceToKeep;

	public float speed;
	private Rigidbody rb;
	public int arrayPointer;
	private int maxArrayValue;
	public string nameTag1;
	public string nameTag2;

	private bool heyBaby;

	private Transform target;

	public bool rayCastHit;

	// Use this for initialization
	void Start () {
		rayCastHit = false;
		rb = GetComponent<Rigidbody>();
		maxArrayValue = (nodeArray.Length);
		SelectTarget (arrayPointer);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//turn to face target
		transform.LookAt(target);
		//cast check ray
		castRay ();
		//if hit check what it hit
		if (rayCastHit == true)
		{
			RaycastHit hit;
			int num;
			//check ray against what stored tag set in editor

				if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hit, Mathf.Infinity) && hit.transform.tag == nameTag1) {
					Debug.Log ("imma not");
					//if hit find the closest member of the array
					//FindClosestArrayMember (arrayPointer);
					SelectTarget (arrayPointer);
				}
				bool heyBaby  = (Random.value > 0.5f);
			if (heyBaby == true) 
			{
				if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hit, Mathf.Infinity) && hit.transform.tag == nameTag2) {
					Debug.Log ("imma not");
					//if hit find the closest member of the array
					//FindClosestArrayMember (arrayPointer);
					SelectTarget (arrayPointer);
				}
			}
			//if not that tag then proceed as normal
			else {
					//move towards the target
					moveStep ();
				}

		}
		//reset rayCastHit to false to reset for next loop
		rayCastHit = false;

		//check if we are withing safe distance of target
		bool inRange = false;
		InRange(inRange);
		//if in range of target, give a new target, set inrange to false after
		if (inRange == true) 
		{
			SelectTarget (arrayPointer);
			inRange = false;
		}
		//if the initial raycast never hit anything either also move as normal
		moveStep();
	}

	//check if the ai is within a range of the target
	bool InRange(bool inRange)
	{
		Debug.Log ("checking rangeee");
		Vector3 position1 = this.transform.position;
		Vector3 position2 = nodeArray [arrayPointer].transform.position;
		bool key1 = false;
		bool key2 = false;

		if (position1.x > position2.x - 5 && position1.x < position2.x + 5) 
		{
			key1 = true;
		}
		if (position1.z > position2.z - 5 && position1.z < position2.z + 5) 
		{
			key2 = true;
		}
		if (key1 == true & key2 == true)
		{
			inRange = true;
		}

		return inRange;
	}

	//movement function just moves towards the target
	void moveStep()
	{
		Debug.Log ("we do a move");
		// Move our position a step closer to the target.
		float step =  speed * Time.deltaTime; // calculate distance to move
		this.transform.position = Vector3.MoveTowards(transform.position, nodeArray[arrayPointer].transform.position, step);

		//rb.velocity = transform.forward * speed;

		float distance = Vector3.Distance (transform.position, nodeArray[arrayPointer].transform.position);

		this.transform.Translate (Vector3.forward * (3.0f * (distance / DistanceToKeep)) * Time.deltaTime);
		if (distance < DistanceToKeep) 
		{
			this.transform.Translate (Vector3.forward * (2.0f * (distance / DistanceToKeep)) * Time.deltaTime);
		}
	}

	//raycast to see if we will collide with an object
	void castRay()
	{
		int layerMask = 1 << 1;
		layerMask = ~layerMask;

		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hit, Mathf.Infinity, layerMask)) {
			Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.forward) * hit.distance, Color.yellow);
			Debug.Log ("did hit");
			rayCastHit = true;
		}
		else 
		{
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
			//Debug.Log("Did not Hit");
		}

	}

	//find an int for the position of the array member we want to move to
	int FindClosestArrayMember(int arrayPointer)
	{
		for (int i = 0; i < maxArrayValue; i++) 
		{
			float closest;
			float distance = Vector3.Distance(this.transform.position, nodeArray[i].transform.position);
			Vector3 diff = nodeArray[i].transform.position - this.transform.position;
			float curDistance = diff.sqrMagnitude;

			if(distance < curDistance)
			{
				//closest = distance;
				target = nodeArray[i].transform;
				arrayPointer = i;
			}
		}
	return arrayPointer;
	}

	//inital randomly selected node that the ai wants to get to
	int SelectTarget(int arrayPointer)
	{
		arrayPointer = Random.Range (0, nodeArray.Length);
		target = nodeArray [arrayPointer].transform;
	return arrayPointer;
	}
}
