using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    // Property to get the item currently in the slot
    public GameObject Item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }

            return null;
        }
    }

    // Triggered when an item is dropped onto the slot
    public void OnDrop(PointerEventData eventData)
    {
        // Check if there is no item in the slot already
        if (!Item)
        {
            StartCoroutine(equip());








            // Set the dragged item as the child of this slot
            DragDrop.itemBeingDragged.transform.SetParent(transform);
            DragDrop.itemBeingDragged.transform.localPosition = new Vector2(0, 0);

            // Check if the slot is not a quick slot
            if (!transform.CompareTag("Quick_Slot"))
            {
                // Update the item's status inside the quick slot
                DragDrop.itemBeingDragged.GetComponent<InventoryItem>().isInsideQuickSlot = false;
                // Recalculate the inventory list
                InventorySystem.Instance.RecalculateList();
            }

            // Check if the slot is a quick slot
            if (transform.CompareTag("Quick_Slot"))
            {
                // Update the item's status inside the quick slot
                DragDrop.itemBeingDragged.GetComponent<InventoryItem>().isInsideQuickSlot = true;
                // Recalculate the inventory list
                InventorySystem.Instance.RecalculateList();
            }
        }
    }
    IEnumerator equip()
    {
        yield return new WaitForSeconds(0.1f);
        Sound_Manager.Instance.PlaySound(Sound_Manager.Instance.dropitemSound);
    }
}
