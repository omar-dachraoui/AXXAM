using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint 
{
    // Fields to store information about the blueprint
    public string itemName;     // Name of the crafted item
    public string Req1;         // Name of the first requirement
    public string Req2;         // Name of the second requirement

    public int Req1amount;      // Amount required of the first requirement
    public int Req2amount;      // Amount required of the second requirement
    public int numOfRequirement; // Total number of requirements
    public int numberofproducedItems;           // Number of items produced

    // Constructor to initialize the blueprint with specified values
    public Blueprint(string name,int producedItems, int reqNum, string R1, int R1num, string R2, int R2num)
    {
        itemName = name;             // Set the item name
        numOfRequirement = reqNum; 
        numberofproducedItems = producedItems;   // Set the total number of requirements
        Req1 = R1;                   // Set the name of the first requirement
        Req2 = R2;                   // Set the name of the second requirement
        Req1amount = R1num;          // Set the amount required for the first requirement
        Req2amount = R2num;          // Set the amount required for the second requirement
    }
}
