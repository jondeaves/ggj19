using UnityEngine;

public class LootItem : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Display name of loot item")]
    public string Name = "Vase";

    [SerializeField]
    [Tooltip("Monetary value of the item")]
    public float Value = 100.0f;
}
