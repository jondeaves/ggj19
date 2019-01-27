using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seekingAi : MonoBehaviour {

	public string searchTag1;
	public string searchTag2;

	private GameObject closetMissle;
	private Transform target;
	public GameObject avoid; 
	public GameObject[] safeNodes;
	public int maxNodes;
	public float safeDistance;
	public bool lastVisitedNode;

	private int nodeToGoTo;

	public int nodeNumber;

	void Start()
	{
		//remvoing this in favour of just selecting a member of the nodes
		//closetMissle = FindClosestEnemy();
		nodeNumber = Random.Range (0, safeNodes.Length);
		closetMissle = safeNodes [nodeNumber];
		avoid = FindClosestAlly ();

		if(closetMissle)
			target = closetMissle.transform;
	}  

	void Update()
	{  
		transform.LookAt(target);

		CheckForObsticle ();

		float distance = Vector3.Distance (transform.position, closetMissle.transform.position);

		float distanceBetweenSeekers = Vector3.Distance (transform.position, avoid.transform.position);

		if (safeDistance < distanceBetweenSeekers) 
		{
			target = avoid.transform;
			//transform.Translate(Vector3.back * 1.0f * Time.deltaTime);
		} 
		else 
		{
			target = closetMissle.transform;
		}

		//checks to see which target is actually closer and then pursues that target
		closetMissle = FindClosestEnemy();
		avoid = FindClosestAlly ();

		if (distance > safeDistance) {
			transform.Translate (Vector3.forward * (3.0f * (distance / safeDistance)) * Time.deltaTime);
		}
		else 
		{
			transform.Translate (Vector3.forward * (2.0f * (distance / safeDistance)) * Time.deltaTime);
		}


	}

	void CheckForObsticle()
	{
		//cast a ray to the safedistance, if obsticle is presenent move to node
		RaycastHit hit;

		if (Physics.Raycast (transform.position, -Vector3.up, out hit, safeDistance)) 
		{
			if (hit.transform.tag == "obsticle")
			{
				MoveToNodeInstead ();
			}
			target = safeNodes [nodeToGoTo].transform;
		}

	}

	void MoveToNodeInstead()
	{

	for (int i = 0; i < maxNodes; i++) 
	{
		float closestDistanceToUs;
		//float closestDistanceToIt;
		bool closestToUsSet = false;

		float distance = Vector3.Distance(this.transform.position, safeNodes[i].transform.position);
		float distanceFromTarget = Vector3.Distance (closetMissle.transform.position, safeNodes [i].transform.position);

		Vector3 position = transform.position;
		Vector3 diff = safeNodes[i].transform.position - position;
		float curDistance = diff.sqrMagnitude;

		//find the closest node to us (check if we are on a node)
		if (this.transform.position == safeNodes [i].transform.position) 
		{
			//if we have been to this node last we want to pick a new node to try instead
			if (lastVisitedNode == false)
			{
				int homeNode = i;
				lastVisitedNode = true;
			} 
			else 
			{
				for (int r = 0; r < maxNodes; r++)
				{
					float distance2 = Vector3.Distance(this.transform.position, safeNodes[r].transform.position);
					if (r == i) 
					{

					} 
					//if (this.transform.position == safeNodes [r].transform.position)
					//{
					//
					//} 
					else if (distance2 < curDistance)
					{
						closestDistanceToUs = distance2;
						nodeToGoTo = r;
						closestToUsSet = true;	
					}
				}
			}
		}
			else if (distance < curDistance) 
			{
				if (closestToUsSet == false) 
				{
					closestDistanceToUs = distance;
					nodeToGoTo = i;
				}
			}
			//////////////////////////////
		
			/* i decided i wouldnt need this after all
			//find closest node to the target (check if they are on a node) (i dont think i'll need this but still)
		if (closetMissle.transform.position == safeNodes [i].transform.position) 
		{
			int targetHomeNode = i;
		} 
		else if (distanceFromTarget < closestDistanceToIt)
		{
			closestDistanceToIt = distanceFromTarget;
		}
			*/ 

			////////////////////////////
	}
}


	//finds the closest other pursuer (finds an object with the tag it should try to avoid it
	GameObject FindClosestAlly()
	{
		GameObject[] gca;
		gca = GameObject.FindGameObjectsWithTag (searchTag1);

		GameObject closestAlly = null;
		float distance = Mathf.Infinity;

		Vector3 position = transform.position;

		foreach (GameObject go2 in gca) 
		{
			Vector3 diff = go2.transform.position - position;
			float curDistance = diff.sqrMagnitude;

			if (curDistance < distance) 
			{
				closestAlly = go2;
				distance = curDistance;
			}
		}
		return closestAlly;
	}

	//finds the nearest object to chase
	GameObject FindClosestEnemy()
	{
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag(searchTag2);

		GameObject closest = null;
		float distance = Mathf.Infinity;

		Vector3 position = transform.position;

		foreach(GameObject go in gos)
		{
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;

			if(curDistance < distance)
			{
				closest = go;
				distance = curDistance;
			}
		}

		return closest;
	}


}