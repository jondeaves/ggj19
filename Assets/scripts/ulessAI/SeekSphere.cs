using UnityEngine;
using System.Collections;

public class SeekSphere : MonoBehaviour {

	public string searchTag;
	private GameObject closetMissle;
	private Transform target;
	public GameObject avoid; 
	public float safeDistance;

	void Start()
	{
		closetMissle = FindClosestEnemy();

		avoid = FindClosestAlly ();

		if(closetMissle)
			target = closetMissle.transform;
	}  

	void Update()
	{  
		transform.LookAt(target);

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

	//finds the closest other pursuer
	GameObject FindClosestAlly()
	{
		GameObject[] gca;
		gca = GameObject.FindGameObjectsWithTag (searchTag);

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
		gos = GameObject.FindGameObjectsWithTag(searchTag);

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

