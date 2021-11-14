using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public int itemCount = 0;
    [SerializeField]public Image itemImage;

    [SerializeField]
    private Text text_Count;

    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = _item.itemImage;
        text_Count.text = itemCount.ToString(); 
    }

    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            ClearSlot();    
        }
    }

    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        text_Count.text = "0";
    }

    public void UseItem()
    {
        if(item.itemType == Item.ItemType.Posion)
        {
            PlayerMove hp = GetComponent<PlayerMove>();
            hp.player_Hp += 10;
        }
    }
}
