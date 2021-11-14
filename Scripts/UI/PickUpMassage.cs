using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpMassage : MonoBehaviour
{
    [SerializeField]
    private float range;
    private bool pickupActivated = false;
    private RaycastHit hitInfo;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Inventory inventory;

    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            CheckItem();
            CanPickUp();
        }
    }
    
    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
        }
        else
        {
            ItemInfoDisappear();
        }
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = "Press (f), If you want";
    }

    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

    private void CanPickUp()
    {
        if(pickupActivated)
        {
            if(hitInfo.transform != null)
            {
                inventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
            }
        }
        else
        {
            return;
        }
    }
}
