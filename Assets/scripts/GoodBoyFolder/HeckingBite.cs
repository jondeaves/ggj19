using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeckingBite : MonoBehaviour {

	bool biteRange;
	GameObject gameObj;

	[SerializeField]
	private readonly int PlayerNumber = 1;

	// Use this for initialization
	void Start ()
	{
		biteRange = false;
	}
	
	// Update is called once per frame
	void Update () 
		{

		if (Input.GetButtonUp("Action " + PlayerNumber))
		{
			Debug.LogFormat ("bork");
			if (gameObj.tag == "thief")
			{
				gameObj.SetActive(false);
			}
		}
	}

	void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.tag == "thief")
		{
			Debug.Log ("smellyBoy");
			biteRange = true;
			gameObj = collision.gameObject;
		}

	}
}
