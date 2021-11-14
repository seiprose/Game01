using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Posion,
        Capsule,
        Throw
    }

    public ItemType itemType;
    public GameObject ItemPrefab;
    public Sprite itemImage;
    public string itemName;
}
