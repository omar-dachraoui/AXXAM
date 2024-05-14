using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSystem : MonoBehaviour
{
    // Singleton instance of EquipSystem
    public static EquipSystem Instance { get; private set; }

    [Header("UI")]
    public GameObject quickSlotsPanel;
    public GameObject numberHolder;

    // Lists to store quick slots and equipped item names
    private List<GameObject> quickSlotsList = new List<GameObject>();

    // Currently selected slot and its associated game object
    public int selectedSlot = -1;
    private GameObject selectedSlotObject;

    // Constant for the maximum number of slots
    private const int MaxSlots = 7;
    public GameObject tool_Holder;
    public GameObject selectedItemModel;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Populate the list of quick slots during initialization
        PopulateSlotList();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle input for selecting quick slots
        HandleInput();
    }

    // Method to handle number key input for selecting quick slots
    private void HandleInput()
    {
        for (int i = 1; i <= MaxSlots; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                selectQuickslot(i);
            }
        }
    }

    // Method to handle the selection of a quick slot
    public void selectQuickslot(int slotNumber)
    {
        // Check if the selected slot is full
        if (checkIfSlotIsFull(slotNumber))
        {
            if (selectedSlot != slotNumber)
            {
                // Set the selected slot and update the UI
                selectedSlot = slotNumber;
                if (selectedSlotObject != null)
                {
                    selectedSlotObject.GetComponent<InventoryItem>().isselected = false;
                }
                selectedSlotObject = getSelectedItem(slotNumber);
                selectedSlotObject.GetComponent<InventoryItem>().isselected = true;
                SetEquippedModel(selectedSlotObject);

                // Change color of the selected slot in the UI
                foreach (Transform child in numberHolder.transform)
                {
                    child.Find("Text").GetComponent<Text>().color = Color.gray;
                }
                Text toBeChanged = numberHolder.transform.Find("number" + slotNumber).Find("Text").GetComponent<Text>();
                toBeChanged.color = Color.white;
            }
            else
            {
                // Deselect the currently selected slot
                selectedSlot = -1;
                if (selectedSlotObject != null)
                {
                    selectedSlotObject.GetComponent<InventoryItem>().isselected = false;
                    selectedSlotObject = null;
                }
                if (selectedItemModel != null)
                {
                    DestroyImmediate(selectedItemModel.gameObject);
                    selectedItemModel = null;
                }
                // Change color of the selected slot in the UI
                foreach (Transform child in numberHolder.transform)
                {
                    child.Find("Text").GetComponent<Text>().color = Color.gray;
                }
            }
        }
    }

    // Method to set the equipped model based on the selected item
    private void SetEquippedModel(GameObject selectedSlotObject)
    {
        // Destroy any existing equipped model
        if (selectedItemModel != null)
        {
            DestroyImmediate(selectedItemModel.gameObject);
            selectedItemModel = null;
        }

        // Get the name of the selected item without the "(Clone)" suffix
        string selectedSlotObjectName = selectedSlotObject.name.Replace("(Clone)", "");
        // Instantiate the corresponding model and set its position and rotation
        selectedItemModel = Instantiate(Resources.Load<GameObject>(selectedSlotObjectName + "_Model"), new Vector3(0.3f, -0.2f, 0.3f), Quaternion.Euler(7, 106f, 20f));
        selectedItemModel.transform.SetParent(tool_Holder.transform, false);
    }

    // Method to retrieve the game object associated with the selected item
    private GameObject getSelectedItem(int number)
    {
        return quickSlotsList[number - 1].transform.GetChild(0).gameObject;
    }

    // Method to check if a specific quick slot is full
    public bool checkIfSlotIsFull(int slotNumber)
    {
        return quickSlotsList[slotNumber - 1].transform.childCount > 0;
    }

    // Method to populate the list of quick slots
    private void PopulateSlotList()
    {
        foreach (Transform child in quickSlotsPanel.transform)
        {
            if (child.CompareTag("Quick_Slot"))
            {
                quickSlotsList.Add(child.gameObject);
            }
        }
    }

    // Method to add an item to the quick slots
    public void AddToQuickSlots(GameObject itemToEquip)
    {
        // Find the next available slot and set the item's transform
        GameObject availableSlot = FindNextEmptySlot();
        itemToEquip.transform.SetParent(availableSlot.transform, false);

        // Recalculate the inventory list (assuming InventorySystem is another class)
        InventorySystem.Instance.RecalculateList();
    }

    // Method to find the next available empty slot
    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        // Return a new empty GameObject if no slots are available (this may need improvement)
        return new GameObject();
    }

    // Method to check if all quick slots are full
    public bool CheckIfFull()
    {
        // Counter to track the number of filled slots
        int counter = 0;

        // Count the number of filled slots
        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }

        // Check if all slots are filled
        return counter == MaxSlots;
    }
}
