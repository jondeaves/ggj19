using UnityEngine;
using System.Collections;

public class HideSphere : MonoBehaviour {

	public string searchTag;
	public GameObject closetMissle;
	private Transform target;

	float distanceToWander;
	public float safeDistance;

	void Start()
	{
		closetMissle = FindClosestEnemy();

		if(closetMissle)
			target = closetMissle.transform;
	}  

	void Update()
	{  
		//finds nearest object to hide and seek
		closetMissle = FindClosestEnemy ();

		transform.LookAt(target);

		float distance = Vector3.Distance (transform.position, closetMissle.transform.position);

		//if target is within a certain distance object flees, otherwise it just seeks (hide and seek)
		if (distance > safeDistance)
		{
			transform.Translate (Vector3.forward * 1.0f * Time.deltaTime);
		}
		else 
		{
			transform.Translate (Vector3.back * 2.0f * Time.deltaTime);
		}
	}

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

