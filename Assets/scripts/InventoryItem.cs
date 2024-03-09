using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    // Flag indicating if the item is trashable
    public bool isTrashable;

    // UI elements for item information
    private GameObject itemInfoUI;
    private Text itemInfoUI_itemName;
    private Text itemInfoUI_itemDescription;
    private Text itemInfoUI_itemFunctionality;

    // Item details
    public string thisName, thisDescription, thisFunctionality;

    // Consumption properties
    private GameObject itemPendingConsumption;
    public bool isConsumable;
    public float healthEffect;
    public float caloriesEffect;
    public float hydrationEffect;

    // Equipment properties
    public bool isEquipment;
    public bool isInsideQuickSlot;
    private GameObject itemPendingEquip;
    public bool isselected;

    private void Start()
    {
        // Initialize UI elements for item information
        itemInfoUI = InventorySystem.Instance.ItemInfoUi;
        itemInfoUI_itemName = itemInfoUI.transform.Find("Item_name").GetComponent<Text>();
        itemInfoUI_itemDescription = itemInfoUI.transform.Find("Item_Description").GetComponent<Text>();
        itemInfoUI_itemFunctionality = itemInfoUI.transform.Find("Item_functionallity").GetComponent<Text>();
    }

    void Update()
    {
        // Disable DragDrop script when the item is selected
        if (isselected)
        {
            gameObject.GetComponent<DragDrop>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<DragDrop>().enabled = true;
        }
    }

    // Triggered when the mouse enters into the area of the item that has this script.
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Display item information UI
        itemInfoUI.SetActive(true);
        itemInfoUI_itemName.text = thisName;
        itemInfoUI_itemDescription.text = thisDescription;
        itemInfoUI_itemFunctionality.text = thisFunctionality;
    }

    // Triggered when the mouse exits the area of the item that has this script.
    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide item information UI
        itemInfoUI.SetActive(false);
    }

    // Triggered when the mouse is clicked over the item that has this script.
    public void OnPointerDown(PointerEventData eventData)
    {
        // Right Mouse Button Click on
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Consumable item handling
            if (isConsumable)
            {
                // Setting this specific game object to be the item we want to destroy later
                itemPendingConsumption = gameObject;
                consumingFunction(healthEffect, caloriesEffect, hydrationEffect);
            }
            // Equipment item handling
            if (isEquipment && isInsideQuickSlot == false && EquipSystem.Instance.CheckIfFull() == false)
            {
                EquipSystem.Instance.AddToQuickSlots(gameObject);
                isInsideQuickSlot = true;
            }
        }
    }

    // Triggered when the mouse button is released over the item that has this script.
    public void OnPointerUp(PointerEventData eventData)
    {
        // Right Mouse Button Release
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Consumable item handling
            if (isConsumable && itemPendingConsumption == gameObject)
            {
                // Destroy the consumable item
                DestroyImmediate(gameObject);
                InventorySystem.Instance.RecalculateList();
                CraftingSystem.Instance.RefrechNeededItem();
            }
        }
    }

    // Function to handle item consumption effects
    private void consumingFunction(float healthEffect, float caloriesEffect, float hydrationEffect)
    {
        // Hide item information UI
        itemInfoUI.SetActive(false);

        // Calculate and apply health, calories, and hydration effects
        healthEffectCalculation(healthEffect);
        caloriesEffectCalculation(caloriesEffect);
        hydrationEffectCalculation(hydrationEffect);
    }

    // Function to calculate health effect
    private static void healthEffectCalculation(float healthEffect)
    {
        // --- Health --- //
        float healthBeforeConsumption = playerstate.Instance.currenthealth;
        float maxHealth = playerstate.Instance.maxhealth;

        if (healthEffect != 0)
        {
            if ((healthBeforeConsumption + healthEffect) > maxHealth)
            {
                playerstate.Instance.setHealth(maxHealth);
            }
            else
            {
                playerstate.Instance.setHealth(healthBeforeConsumption + healthEffect);
            }
        }
    }

    // Function to calculate calories effect
    private static void caloriesEffectCalculation(float caloriesEffect)
    {
        // --- Calories --- //
        float caloriesBeforeConsumption = playerstate.Instance.currentfood;
        float maxCalories = playerstate.Instance.maxfood;

        if (caloriesEffect != 0)
        {
            if ((caloriesBeforeConsumption + caloriesEffect) > maxCalories)
            {
                playerstate.Instance.setfood(maxCalories);
            }
            else
            {
                playerstate.Instance.setfood(caloriesBeforeConsumption + caloriesEffect);
            }
        }
    }

    // Function to calculate hydration effect
    private static void hydrationEffectCalculation(float hydrationEffect)
    {
        // --- Hydration --- //
        float hydrationBeforeConsumption = playerstate.Instance.currenthydration;
        float maxHydration = playerstate.Instance.maxhydration;

        if (hydrationEffect != 0)
        {
            if ((hydrationBeforeConsumption + hydrationEffect) > maxHydration)
            {
                playerstate.Instance.setHydration(maxHydration);
            }
            else
            {
                playerstate.Instance.setHydration(hydrationBeforeConsumption + hydrationEffect);
            }
        }
    }
}
