using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class InventorySystem : MonoBehaviour
{
    // Singleton pattern to ensure only one instance of the InventorySystem exists
    public static InventorySystem Instance { get; set; }

    // List of inventory slots
    public List<GameObject> slotList = new List<GameObject>();

    // List of items in the inventory
    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;
    private GameObject whatSlotToequip;

    // Reference to the inventory UI
    public GameObject inventoryScreenUI;

    // Flag to track if the inventory is currently open
    public bool isOpen;

    // Pickup alert UI elements
    public GameObject pickupalert;
    public Text PickupName;
    public Image Pickupimage;

    // Item info UI
    public GameObject ItemInfoUi;

    private void Awake()
    {
        // Singleton implementation: destroy duplicate instances
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        // Initialize the inventory as closed
        isOpen = false;
        populateSlotList();
        Cursor.visible = false;
    }

    private void populateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }

    void Update()
    {
        // Check for input to toggle the inventory screen
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            // Open the inventory screen
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
            Cursor.visible = true;
            SelectionManager.Instance.disableselction();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            // Close the inventory screen
            inventoryScreenUI.SetActive(false);
            if (!CraftingSystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                SelectionManager.Instance.enableselction();
                SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
                Cursor.visible = false;
            }
            isOpen = false;
        }
    }

    public void AddToInventory(string ItemName)
    {
        Sound_Manager.Instance.PlaySound(Sound_Manager.Instance.pickupitemSound);
        whatSlotToequip = findNextEmptySlot();
        itemToAdd = Instantiate(Resources.Load<GameObject>(ItemName+"_UI"), whatSlotToequip.transform.position, whatSlotToequip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToequip.transform);
        itemList.Add(ItemName);
        triggerPickupPop(ItemName, itemToAdd.GetComponent<Image>().sprite);
    }

    // Display pickup alert for a certain duration
    void triggerPickupPop(string itemname, Sprite itemsprite)
    {
        pickupalert.SetActive(true);
        PickupName.text = itemname;
        Pickupimage.sprite = itemsprite;
        StartCoroutine(DeactivatePickupAlertAfterDelay(2f));
    }

    IEnumerator DeactivatePickupAlertAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        pickupalert.SetActive(false);
    }

    private GameObject findNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public bool CheckSlotAvailable(int emptyNeeded )
    {
        int Emptyslot = 0;
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount <= 0)
            {
                Emptyslot += 1;
            }
        }
        if (Emptyslot >= emptyNeeded)
            {
               return true;
            }
            else
            {
                return false;
            }
        
    }

    public void RemoveItem(string nameToRemove, int amountToRemove)
    {
        int counter = amountToRemove;

        for (var i = slotList.Count - 1; i >= 0; i--)
        {
            if (slotList[i].transform.childCount > 0)
            {
                if (slotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && counter != 0)
                {
                    Destroy(slotList[i].transform.GetChild(0).gameObject);
                    counter-=1;
                }
            }
        }
    }

    // Recalculate the list of items in the inventory
    public void RecalculateList()
    {
        itemList.Clear();

        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name;
                string str2 = "(Clone)";
                string result = name.Replace(str2, "");
                itemList.Add(result);
            }
        }
    }
}
