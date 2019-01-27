using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeckingBite : MonoBehaviour {

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
		if (m_BiteTarget != null && Input.GetButtonUp("Action " + m_PlayerNumber))
		{
			Debug.LogFormat ("bork");
			if (m_BiteTarget.tag == "thief")
			{
				m_BiteTarget.SetActive(false);
			}
			if (m_BiteTarget.tag == "AI")
			{
                m_BiteTarget.SetActive(false);
				this.gameObject.transform.position = m_NaughtyCorner.transform.position;
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
