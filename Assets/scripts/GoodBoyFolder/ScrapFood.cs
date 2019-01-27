using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapFood : MonoBehaviour {

	public bool justAte;

	public int radius;
	GameObject droppedFood;
	public float pullRadius = 5.0f;
	public float pullForce = 2.0f;
	Rigidbody rb;
	public float timeLeft = 30.0f;
	float timeToUse = 0.0f;

	// Use this for initialization
	void Start () 
	{
		timeToUse = timeLeft;
		rb = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//look for dropped food
		droppedFood = GameObject.FindWithTag("food");
		//if close enough move towards food
		if (droppedFood != null && this.transform.position.x > droppedFood.transform.position.x - radius && this.transform.position.z < droppedFood.transform.position.z + radius)
		{

			Vector3 forceDirection = transform.position - GetComponent<Collider>().transform.position;
			rb.AddForce (forceDirection.normalized * pullForce * Time.deltaTime);
		}
		if (droppedFood != null && this.transform.position == droppedFood.transform.position)
		{
			droppedFood.SetActive (false);
			justAte = true;
		}
		if (justAte == true) 
		{
			timeLeft -= Time.deltaTime;
			if(timeLeft < 0)
			{
				justAte = false;
				timeLeft = timeToUse;
			}
		}
	}
			
}
