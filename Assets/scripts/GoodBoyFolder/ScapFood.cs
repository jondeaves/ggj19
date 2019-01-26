using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScapFood : MonoBehaviour {

	public bool justAte;

	public int radius;
	GameObject droppedFood;
	public float pullRadius = 5.0f;
	public float pullForce = 2.0f;
	Rigidbody rb;

	// Use this for initialization
	void Start () 
	{
		rb.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//look for dropped food
		droppedFood = GameObject.FindWithTag("food");
		//if close enough move towards food
		if (this.transform.position.x > droppedFood.transform.position.x - radius && this.transform.position.z < droppedFood.transform.position.z + radius)
		{
			foreach(Collider collider in Physics.OverlapSphere(transform.position, pullRadius));

			Vector3 forceDirection = transform.position - collider.transform.position;
			collider.rb.AddForce (forceDirection.normalized * pullForce * Time.deltaTime);
		}
		if (this.transform.position == droppedFood.transform.position)
		{
			droppedFood.SetActive (false);
		}
	}
			
}
