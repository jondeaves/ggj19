using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Loot;

    [SerializeField]
    private readonly int PlayerNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Action " + PlayerNumber))
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
