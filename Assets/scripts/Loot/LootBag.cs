using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    [SerializeField]
    [Tooltip("All items currently in the thiefs loot bag")]
    public List<GameObject> Loot;

    private int m_PlayerNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerNumber = GetComponent<Movement>().PlayerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Action " + m_PlayerNumber))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.5f);

            foreach (Collider col in hitColliders)
            {
                if (col.tag == "loot" && Loot.FindAll((GameObject obj) => obj == col.gameObject).Count == 0)
                {
                    Debug.Log("Thief got some loot");
                    Loot.Add(col.gameObject);
                    col.gameObject.SetActive(false);
                }
            }
        }
    }

    public void AddLoot(GameObject obj)
    {
        if (obj.tag == "loot")
        {
            Loot.Add(obj);
        }
    }
}
