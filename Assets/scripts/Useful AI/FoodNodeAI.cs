using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodNodeAI : MonoBehaviour {

	public GameObject[] nodeArray;
	public int arrayPointer;
	private int maxArrayValue;

	private Transform target;

	public float timeLeft = 45.0f;
	float timeToUse = 0.0f;


	// Use this for initialization
	void Start () 
	{
		timeToUse = timeLeft;
		maxArrayValue = (nodeArray.Length);
	}
	
	// Update is called once per frame
	void Update ()
	{
		SelectTarget (arrayPointer);
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.GetComponent<Collider> ().isTrigger = true;
		cube.tag = "food";
		cube.transform.position = nodeArray[arrayPointer].transform.position;
	}

	int SelectTarget(int arrayPointer)
	{

		timeLeft -= Time.deltaTime;
		if(timeLeft < 0)
		{
			arrayPointer = Random.Range (0, nodeArray.Length);
			target = nodeArray [arrayPointer].transform;
			return arrayPointer;
			timeLeft = timeToUse;
		}
		return arrayPointer;

	}
}
