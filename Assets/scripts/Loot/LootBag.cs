using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    [SerializeField]
    [Tooltip("All items currently in the thiefs loot bag")]
    public List<GameObject> Loot;
}
