using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class TrashSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Reference to the UI element for trash confirmation
    public GameObject trashAlertUI;

    // Text component to modify the alert message
    private Text textToModify;

    // Sprite for the closed and opened trash slots
    public Sprite trash_closed;
    public Sprite trash_opened;

    // Image component of the background for sprite changes
    private Image imageComponent;

    // Buttons for confirmation and cancellation of deletion
    Button YesBTN, NoBTN;

    // Reference to the dragged item
    GameObject draggedItem
    {
        get
        {
            return DragDrop.itemBeingDragged;
        }
    }

    // Reference to the item to be deleted
    GameObject itemToBeDeleted;

    // Property to get the item name without "(Clone)"
    public string itemName
    {
        get
        {
            string name = itemToBeDeleted.name;
            string toRemove = "(Clone)";
            string result = name.Replace(toRemove, "");
            return result;
        }
    }

    // Initialization
    void Start()
    {
        // Get the Image component of the background
        imageComponent = transform.Find("background").GetComponent<Image>();

        // Get the Text component for modifying the alert message
        textToModify = trashAlertUI.transform.Find("Text").GetComponent<Text>();

        // Get the Yes button and attach a listener for item deletion
        YesBTN = trashAlertUI.transform.Find("YES").GetComponent<Button>();
        YesBTN.onClick.AddListener(delegate { DeleteItem(); });

        // Get the No button and attach a listener for canceling deletion
        NoBTN = trashAlertUI.transform.Find("NO").GetComponent<Button>();
        NoBTN.onClick.AddListener(delegate { CancelDeletion(); });
    }

    // Triggered when an item is dropped onto the trash slot
    public void OnDrop(PointerEventData eventData)
    {
        if (draggedItem.GetComponent<InventoryItem>().isTrashable == true)
        {
            // Set the item to be deleted to the dragged item
            itemToBeDeleted = draggedItem.gameObject;

            // Show the deletion confirmation alert
            StartCoroutine(notifyBeforeDeletion());
        }
    }

    // Coroutine to show the deletion confirmation alert
    IEnumerator notifyBeforeDeletion()
    {
        trashAlertUI.SetActive(true);
        textToModify.text = "Throw away this " + itemName + "?";
        yield return new WaitForSeconds(1f);
    }

    // Triggered when the user cancels the deletion
    private void CancelDeletion()
    {
        // Change the sprite to closed trash
        imageComponent.sprite = trash_closed;
        // Hide the deletion confirmation alert
        trashAlertUI.SetActive(false);
    }

    // Triggered when the user confirms the deletion
    private void DeleteItem()
    {
        // Change the sprite to closed trash
        imageComponent.sprite = trash_closed;
        // Destroy the item and update inventory and crafting
        DestroyImmediate(itemToBeDeleted.gameObject);
        InventorySystem.Instance.RecalculateList();
        //BaseScreenUI.Instance.RefrechNeededItem();
        QuestManager.Instance.RefreshedTrackedQuestList();
        // Hide the deletion confirmation alert
        trashAlertUI.SetActive(false);
    }

    // Triggered when the mouse pointer enters the trash slot
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change the sprite to opened trash if a valid item is being dragged
        if (draggedItem != null && draggedItem.GetComponent<InventoryItem>().isTrashable == true)
        {
            imageComponent.sprite = trash_opened;
        }
    }

    // Triggered when the mouse pointer exits the trash slot
    public void OnPointerExit(PointerEventData eventData)
    {
        // Change the sprite to closed trash if a valid item is being dragged
        if (draggedItem != null && draggedItem.GetComponent<InventoryItem>().isTrashable == true)
        {
            imageComponent.sprite = trash_closed;
        }
    }
}
