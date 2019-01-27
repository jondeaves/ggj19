using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWorld : MonoBehaviour
{
    private List<GameObject> m_EscapedThiefs;
    private AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        m_EscapedThiefs = new List<GameObject>();
        m_AudioSource = GetComponentInChildren<AudioSource>();
    }

    private void Update()
    {
        if (Debug.isDebugBuild && (Input.GetButtonUp("Cancel")))
        {
            m_AudioSource.time = 55f;
        }

        float timeLeft = m_AudioSource.clip.length - m_AudioSource.time;
        int thiefsRemaining = GameObject.FindGameObjectsWithTag("thief").Length;

        if (timeLeft <= 0 || thiefsRemaining == 0)
        {

            float totalValue = 0f;

            // Tally up the loot
            foreach (GameObject thief in m_EscapedThiefs)
            {
                foreach(GameObject loot in thief.GetComponent<LootBag>().Loot)
                {
                    totalValue  += loot.GetComponent<LootItem>().Value;
                }
            }

            GameData.valueOfStolenGoods = totalValue;

            if (totalValue > 0)
            {
                SceneManager.LoadScene("Defeat");
                Debug.Log("Bad boy! Someone has gotten away with £" + totalValue + " worth of our family memories!");
            }
            else
            {
                SceneManager.LoadScene("Victory");
                Debug.Log("Such a Hackin Good Boy, you've kept the house safe from thiefs.");
            }
        }
    }

    public void AddEscapee(GameObject obj)
    {
        if (obj.tag == "thief")
        {
            Debug.Log(obj.tag + " has escaped");
            m_EscapedThiefs.Add(obj);
        }
    }
}
