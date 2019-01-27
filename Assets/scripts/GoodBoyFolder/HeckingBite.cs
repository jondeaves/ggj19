using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeckingBite : MonoBehaviour {
	public bool biteRange;
	GameObject gameObj;
	public ParticleSystem barkParticleSystem;

	public GameObject naughtyCorner;

	[FMODUnity.EventRef]
	public string biteEventFMOD;

	[FMODUnity.EventRef]
	public string barkEventFMOD;

	[FMODUnity.EventRef]
	public string wrongBiteEventFMOD;

	private int m_PlayerNumber;

	// Use this for initialization
	void Start ()
	{
		biteRange = false;
		m_PlayerNumber = GetComponent<Movement>().PlayerNumber;
	}

	// Update is called once per frame
	void Update ()
    {
		if (Input.GetButtonDown("Action " + m_PlayerNumber))
		{
			FMODUnity.RuntimeManager.PlayOneShot(barkEventFMOD, transform.position);
			barkParticleSystem.Play();

			if (gameObj != null)
			{
				string tag = gameObj.tag;

				if (tag == "thief")
				{
					FMODUnity.RuntimeManager.PlayOneShot(biteEventFMOD, transform.position);
					gameObj.SetActive(false);
				}
				if (tag == "AI")
				{
					FMODUnity.RuntimeManager.PlayOneShot(wrongBiteEventFMOD, transform.position);
					this.gameObject.transform.position = naughtyCorner.transform.position;
				}

				gameObj = null;
			}
		}
	}

	void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.tag == "thief")
		{
			//Debug.Log ("smellyBoy");
			biteRange = true;
			gameObj = collision.gameObject;
		}
		if (collision.gameObject.tag == "AI")
		{
			biteRange = true;
			gameObj = collision.gameObject;
		}
	}
}
