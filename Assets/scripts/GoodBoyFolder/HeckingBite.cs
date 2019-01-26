using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeckingBite : MonoBehaviour {

	[SerializeField]
	private readonly int PlayerNumber = 1;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () 
		{

			if (Input.GetButtonUp("Action " + PlayerNumber))
			{
				Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.5f);

				foreach (Collider col in hitColliders)
				{
					if (col.tag == "theif")
					{
						col.gameObject.SetActive(false);
					}
				}
			}
	}
}
