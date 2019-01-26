using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour
{
    private List<GameObject> m_EscapedThiefs;

    // Start is called before the first frame update
    void Start()
    {
        m_EscapedThiefs = new List<GameObject>();
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
