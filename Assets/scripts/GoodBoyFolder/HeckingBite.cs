using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeckingBite : MonoBehaviour {
	public ParticleSystem barkParticleSystem;

	[FMODUnity.EventRef]
	public string biteEventFMOD;

	[FMODUnity.EventRef]
	public string barkEventFMOD;

	[FMODUnity.EventRef]
	public string wrongBiteEventFMOD;


	private GameObject m_BiteTarget;
	private GameObject m_NaughtyCorner;
	private int m_PlayerNumber;

	// Use this for initialization
	void Start ()
	{
		m_PlayerNumber = GetComponent<MovementV2>().PlayerNumber;
		m_NaughtyCorner = GameObject.FindWithTag("NaughtyCorner");
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown("Action " + m_PlayerNumber))
		{
			FMODUnity.RuntimeManager.PlayOneShot(barkEventFMOD, transform.position);
			barkParticleSystem.Play();

			if (m_BiteTarget != null)
			{
				string tag = m_BiteTarget.tag;

				if (tag == "thief")
				{
					FMODUnity.RuntimeManager.PlayOneShot(biteEventFMOD, transform.position);
					m_BiteTarget.SetActive(false);
				}
				if (tag == "AI")
				{
					FMODUnity.RuntimeManager.PlayOneShot(wrongBiteEventFMOD, transform.position);
					this.gameObject.transform.position = m_NaughtyCorner.transform.position;
				}

				m_BiteTarget = null;
			}
		}
	}

	void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.tag == "thief")
		{
			m_BiteTarget = collision.gameObject;
		}
		if (collision.gameObject.tag == "AI")
		{
			m_BiteTarget = collision.gameObject;
		}
	}
}
