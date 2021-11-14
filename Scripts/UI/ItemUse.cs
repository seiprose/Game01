using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    PlayerMove pm;
    public GameObject Trap;
    public GameObject go_SlotsParent;
    public Slot[] slots;

    void Start()
    {
        pm = GetComponent<PlayerMove>();
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem(0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseItem(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseItem(2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseItem(3);
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            UseItem(4);
        }
    }

    void UseItem(int i)
    {
        if(slots[i].itemCount > 0)
        {
            if(slots[i].item.itemType == Item.ItemType.Posion)
            {
                slots[i].SetSlotCount(-1);
                PlayerMove pm = GetComponent<PlayerMove>();
                pm.player_Hp += 10;
                if(slots[i].itemCount == 0)
                {
                    slots[i].ClearSlot();
                }
            }
            if(slots[i].item.itemType == Item.ItemType.Capsule)
            {
                slots[i].SetSlotCount(-1);
               
            }
        }
    }
}
