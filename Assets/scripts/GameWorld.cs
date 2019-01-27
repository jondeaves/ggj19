using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWorld : MonoBehaviour
{
    public float m_TimeLeft = 60;
    public float gameTimeElapsedSeconds = 0;
    public float gameDurationSeconds = 60;
    private List<GameObject> m_EscapedThiefs;

    // Start is called before the first frame update
    void Start()
    {
        m_EscapedThiefs = new List<GameObject>();
    }

    private void Update()
    {
        if (Debug.isDebugBuild && (Input.GetButtonUp("Cancel")))
        {
            gameTimeElapsedSeconds = 55f;
        }

        gameTimeElapsedSeconds += Time.deltaTime;
        m_TimeLeft = gameDurationSeconds - gameTimeElapsedSeconds;

        int thiefsRemaining = GameObject.FindGameObjectsWithTag("thief").Length;

        if (m_TimeLeft <= 0 || thiefsRemaining == 0)
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
                if (MusicManager.Instance)
                {
                    MusicManager.Instance.SetMusicState(MusicState.BurglarWin);
                }

                SceneManager.LoadScene("Defeat");
                Debug.Log("Bad boy! Someone has gotten away with £" + totalValue + " worth of our family memories!");
            }
            else
            {
                if (MusicManager.Instance)
                {
                    MusicManager.Instance.SetMusicState(MusicState.DogWin);
                }

                SceneManager.LoadScene("Victory");
                Debug.Log("Such a Heckin Good Boy, you've kept the house safe from thieves.");
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
