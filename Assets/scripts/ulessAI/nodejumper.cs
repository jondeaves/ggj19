using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodejumper : MonoBehaviour {

	public string searchTag;
	public GameObject closest;
	public Transform target;
	float wanderDistance;
	public float safeDistance;
	private bool safeToProceed;

	public GameObject[] nodeArray;
	public int useNodePls; 

	public float speed = 0.1f;
	public Rigidbody rb;
	private bool moving;

	//private float distanceToGo;
	//public int objectToGoTo;
	//public int actualGoal;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody>();
		//actualGoal = Random.Range (0, node.Length);

		closest = FindClosest();

		if(closest)
			target = closest.transform;
	}

	void CheckForObsticle()
	{
		//cast a ray to the safedistance, if obsticle is presenent move to node
		RaycastHit hit;

		if (Physics.Raycast (transform.position, -Vector3.up, out hit, safeDistance)) 
		{
			if (hit.transform.tag == "obsticle")
			{
				CheckForNode ();
			}
			safeToProceed = true;
		}

	}

	void CheckForNode()
	{
		int maxLengh = nodeArray.Length;
		float lastDist = 10.0f;
		for (int i = 0; i < maxLengh; i++)
			{
				float distance = Vector3.Distance(this.transform.position, nodeArray[i].transform.position);
				

			if (distance < lastDist) 
			{
				lastDist = distance;
				useNodePls = i;
			}
			}
	}

	// Update is called once per frame
	void Update () 
	{
		transform.LookAt(target);

		CheckForObsticle ();

		if (safeToProceed == true) {
			float distance = Vector3.Distance (transform.position, closest.transform.position);

			//if target is within a certain distance object flees, otherwise it just seeks
			if (distance > safeDistance) {
				transform.Translate (Vector3.forward * speed * Time.deltaTime);
			} else {
				transform.Translate (Vector3.back * (speed * (distance / safeDistance)) * Time.deltaTime);
			}
		} 
		else 
		{
			target = nodeArray [useNodePls].transform;
			transform.LookAt (target);

			float distance = Vector3.Distance (transform.position, closest.transform.position);

			//if target is within a certain distance object flees, otherwise it just seeks
			if (distance > safeDistance) {
				transform.Translate (Vector3.forward * speed * Time.deltaTime);
			} else {
				transform.Translate (Vector3.back * (speed * (distance / safeDistance)) * Time.deltaTime);
			}
		}

		/* old code
		 //check if ai has made it to it's actual goal
		if (this.transform.position == node [actualGoal].transform.position) 
		{
			//set an actual goal point for the ai
			actualGoal = Random.Range (0, node.Length);
		}

		//check for the closest node
		//CheckClosest ();

		//update rotation
		this.transform.LookAt(node[objectToGoTo].transform);

		moveStep ();

		objectToGoTo = actualGoal;

		if (node[objectToGoTo].transform.position == this.transform.position) 
		{
			objectToGoTo = Random.Range (0, node.Length);
			StartCoroutine (StayaWhile ());
		}
			

		//update rotation
		this.transform.LookAt(node[objectToGoTo].transform);

		moving = true;
		//Debug.Log("moving");

		
		if (moving == true) ;
		{
			speed = speed + Time.deltaTime;
			if (speed > 5.0f)
			{
				//Debug.Log(gameObject.transform.position.x + " : " + speed);
			}
		}


		//play movement animation while moving
		*/
	}

	GameObject FindClosest()
	{
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag (searchTag);

		GameObject closest = null;
		float distance = Mathf.Infinity;

		Vector3 position = transform.position;

		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;

			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}

	/*old
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("obsticle"))
		{
			Debug.Log ("hit one");
			objectToGoTo++;
			if (objectToGoTo > maxNodes)
			{
				objectToGoTo = objectToGoTo - 2;
			}
			//objectToGoTo = Random.Range (0, node.Length);
			moveStep();
		}
	}

	IEnumerator StayaWhile()
	{
		print(Time.time);
		yield return new WaitForSecondsRealtime(5);
		print(Time.time);
	}

	public void moveStep()
	{
		// Move our position a step closer to the target.
		float step =  speed * Time.deltaTime; // calculate distance to move
		this.transform.position = Vector3.MoveTowards(transform.position, node[objectToGoTo].transform.position, step);

		rb.velocity = transform.forward * speed;
	}
	*/
}
