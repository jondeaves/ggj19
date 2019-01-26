using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    [SerializeField]
    public GameObject m_GameWorld;

    private List<GameObject> m_LootBag;
    private int m_PlayerNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerNumber = GetComponent<Movement>().PlayerNumber;
        m_LootBag = GetComponent<LootBag>().Loot;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Action " + m_PlayerNumber))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f);

            foreach (Collider col in hitColliders)
            {
                ThiefActions(col);
                DogActions(col);
            }
        }
    }

    private void ThiefActions(Collider col)
    {
        if(tag != "thief")
        {
            return;
        }

        if (col.tag == "loot" && m_LootBag.FindAll((GameObject obj) => obj == col.gameObject).Count == 0)
        {
            Debug.Log("Thief got some loot");
            m_LootBag.Add(col.gameObject);
            col.gameObject.SetActive(false);
        }

        if (col.tag == "door")
        {
            float timeLeft = m_GameWorld.GetComponentInChildren<AudioSource>().clip.length - m_GameWorld.GetComponentInChildren<AudioSource>().time;
            if (timeLeft <= 10)
            {
                m_GameWorld.GetComponent<GameWorld>().AddEscapee(gameObject);
                gameObject.SetActive(false);
                Debug.Log("I haz won");
            }
            else
            {
                Debug.Log("I is locked in, must wait longer: " + timeLeft);
            }
        }
    }

    private void DogActions(Collider col)
    {
        if (tag != "dog")
        {
            return;
        }

        if (col.tag == "theif")
        {
            col.gameObject.SetActive(false);
        }
    }
}
