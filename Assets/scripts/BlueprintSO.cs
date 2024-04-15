using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Blueprint class to store information about the crafting recipe
[CreateAssetMenu(fileName ="Data", menuName = "ScriptableObjects/BlueprintSO", order = 1)]
public class BlueprintSO : ScriptableObject
{
    [Header("Blueprint Information")]


    [Tooltip("Image of the crafted item")]
    public Sprite image;        // Image of the crafted item


    
    
    // Fields to store information about the blueprint
    [Tooltip("Name of the crafted item")]
    public string itemName;     // Name of the crafted item

    [Tooltip("number of produced item after crafting the")]
    public int numberofproducedItems;           // Number of items produced

    [Tooltip("Index of the item in the crafting list")]
    public int itemIndex;       // Index of the item in the inventory

    [Tooltip("Total number of requirements")]
    public int numOfRequirement; // Total number of requirements
    
    [Tooltip("List of requirements ")]
    public List<string> ReqList  ; // List of requirements

    
    [Tooltip("List of requirement amounts")]
    public List<int> ReqAmountList; // List of requirement amounts



    private void Start()
    {
        ReqList = new List<string>(numOfRequirement);
        ReqAmountList = new List<int>(numOfRequirement);
    }
    
    

    
    
}
