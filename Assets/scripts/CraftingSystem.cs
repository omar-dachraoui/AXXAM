using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class CraftingSystem : MonoBehaviour
{
    

    // UI Screens
    public GameObject CraftingScreeenUI;
    public GameObject ToolScreenUI,survivalScreenUI,refineScreenUI;

    // Inventory item list
    public List<string> InventoryItemList = new List<string>();
    // Blueprint class to store information about the crafting recipe
    [SerializeField] List<BlueprintSO> toolsBlueprintsList = new List<BlueprintSO>();
    [SerializeField] List<BlueprintSO> survivalBlueprintsList = new List<BlueprintSO>();
    [SerializeField] List<BlueprintSO> refineBlueprintsList = new List<BlueprintSO>();

    // Category Buttons
    Button toolsBTN,survivalBTN,refineBTN;

    // Craft Buttons
    Button CraftAxeBTN,craftplankBTN,craftHPBTN;

    // Requirement Text for Axe
   Text AxeReq1, AxeReq2,PlankReq1,HPReq1,HPReq2,HPReq3;

    // Flag indicating if the crafting screen is open
    public bool isOpen;

    
    // Singleton instance
    public static CraftingSystem Instance { get; set; }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // Destroy duplicate instances
            Destroy(gameObject);
        }
        else
        {
            // Set the instance to this object
            Instance = this;
        }
       
    }

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;

        // Assign UI elements and setup button click events
        toolsBTN = CraftingScreeenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });

        //survivalBTN 
        survivalBTN = CraftingScreeenUI.transform.Find("Survival_button").GetComponent<Button>();
        survivalBTN.onClick.AddListener(delegate { OpenSurvivesCategory(); });
          
       //refineBTN
        refineBTN = CraftingScreeenUI.transform.Find("Refine_button").GetComponent<Button>();
        refineBTN.onClick.AddListener(delegate { OpenRefineCategory(); });

      

        
    }

    // Function to craft any item using the provided blueprint
    public void CraftAnyItem(BlueprintSO blueprintToCraft)
    {
        
        //Sound_Manager.Instance.PlaySound(Sound_Manager.Instance.craftSound);
        // Add item into inventory
        
        for(var i = 0; i < blueprintToCraft.numberofproducedItems; i++)
        {
            // Add item into inventory
            InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);
        }

        //Remove resources from inventory based on the blueprint requirements
        RemoveRequirements(blueprintToCraft, blueprintToCraft.numOfRequirement);

        //Refresh inventory list and UI
        
       
        StartCoroutine(calculate());
        
       //BaseScreenUI.Instance.RefrechNeededItem();
        
    }



    private void RemoveRequirements(BlueprintSO blueprintToRemove,int number )
    {
        for (var i = 0; i < number; i++)
        {
            // remove item from inventory
           InventorySystem.Instance.RemoveItem(blueprintToRemove.ReqList[i], blueprintToRemove.ReqAmountList[i]);
        }
        
    }

    // Coroutine to calculate and refresh UI
    public IEnumerator calculate()
    {
        yield return 0; // Optional delay for crafting

        // Refresh inventory list and UI
        InventorySystem.Instance.RecalculateList();
        QuestManager.Instance.RefreshedTrackedQuestList();
        
    }

    // Function to open the tools category in the crafting screen
    private void OpenToolsCategory()
    {
        //refineScreenUI.SetActive(false);
        survivalScreenUI.SetActive(false);
        CraftingScreeenUI.SetActive(false);
        ToolScreenUI.SetActive(true);
    }
    // Function to open the tools category in the refine screen
    private void OpenRefineCategory()
    {
        CraftingScreeenUI.SetActive(false);
        survivalScreenUI.SetActive(false);
        ToolScreenUI.SetActive(false);
        refineScreenUI.SetActive(true);
        
    }
    // Function to open the tools category in the survival screen
    private void OpenSurvivesCategory()
    
    {
        CraftingScreeenUI.SetActive(false);
        ToolScreenUI.SetActive(false);
       refineScreenUI.SetActive(false);
        survivalScreenUI.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check for input to toggle the crafting screen
        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            // Open the crafting screen
            CraftingScreeenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
            Cursor.visible = true;
            SelectionManager.Instance.disableselction();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            // Close the crafting screen
            CraftingScreeenUI.SetActive(false);
            ToolScreenUI.SetActive(false);
            refineScreenUI.SetActive(false);
            survivalScreenUI.SetActive(false);
            if (!InventorySystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                SelectionManager.Instance.enableselction();
                SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
            }
            isOpen = false;
        }
        //BaseScreenUI.Instance.RefrechNeededItem();
    }

    // Function to refresh UI based on required crafting items
  






    public List<BlueprintSO> GetToolBlueprintList()
    {
        return toolsBlueprintsList;
    }

    public List<BlueprintSO> GetSurvivalBlueprintList()
    {
        return survivalBlueprintsList;
    }

    public List<BlueprintSO> GetRefineBlueprintList()
    {
        return refineBlueprintsList;
    }


}



