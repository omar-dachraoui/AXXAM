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
    [SerializeField] List<BlueprintSO> blueprintsList = new List<BlueprintSO>();

    // Category Buttons
    Button toolsBTN,survivalBTN,refineBTN;

    // Craft Buttons
    Button CraftAxeBTN,craftplankBTN;

    // Requirement Text for Axe
   Text AxeReq1, AxeReq2,PlankReq1;

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

        // Setup Axe crafting UI elements
        AxeReq1 = ToolScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<Text>();
        AxeReq2 = ToolScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<Text>();

        CraftAxeBTN = ToolScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        CraftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(blueprintsList[0]); });


        // Setup plank crafting UI elements
        PlankReq1 = refineScreenUI.transform.Find("plank").transform.Find("req1").GetComponent<Text>();
        

        craftplankBTN = refineScreenUI.transform.Find("plank").transform.Find("Button").GetComponent<Button>();
        craftplankBTN.onClick.AddListener(delegate { CraftAnyItem(blueprintsList[1]); });
        
    }

    // Function to craft any item using the provided blueprint
    private void CraftAnyItem(BlueprintSO blueprintToCraft)
    {
        
            //Sound_Manager.Instance.PlaySound(Sound_Manager.Instance.craftSound);
        // Add item into inventory
        
        for(var i = 0; i < blueprintToCraft.numberofproducedItems; i++)
        {
            // Add item into inventory
            InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);
        }








       

        // Remove resources from inventory based on the blueprint requirements
        if (blueprintToCraft.numOfRequirement == 1)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
        }
        else if (blueprintToCraft.numOfRequirement == 2)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req2, blueprintToCraft.Req2amount);
        }

        //Refresh inventory list and UI
        
       
        StartCoroutine(calculate());
        RefrechNeededItem();
        
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
        RefrechNeededItem();
    }

    // Function to refresh UI based on required crafting items
    public void RefrechNeededItem()
    {
        // Count the number of Stone and Stick in the inventory
        int stone_count = 0;
        int stick_count = 0;
        int log_count = 0;
        InventoryItemList = InventorySystem.Instance.itemList;

        foreach (string itemName in InventoryItemList)
        {
            switch (itemName)
            {
                case "Stone":
                    stone_count+=1;
                    break;
                case "Stick":
                    stick_count+=1;
                    break;
                case "log":
                    log_count+=1;
                break;
            }
        }

        

        // Set Axe requirements text
        AxeReq1.text = "3Stone[" + stone_count + "]";
        AxeReq2.text = "3Stick[" + stick_count + "]";

        // Enable or disable Axe crafting button based on requirements
        if (stone_count >= 3 && stick_count >= 3 && InventorySystem.Instance.CheckSlotAvailable(1))
        {
            CraftAxeBTN.gameObject.SetActive(true);
        }
        else
        {
            CraftAxeBTN.gameObject.SetActive(false);
        }





      

        // Set plank requirements text
        PlankReq1.text = "1log[" + log_count + "]";
        
       

        // Enable or disable plank crafting button based on requirements
        if (log_count >= 1 && InventorySystem.Instance.CheckSlotAvailable(2))
        {
            craftplankBTN.gameObject.SetActive(true);
        }
        else
        {
            craftplankBTN.gameObject.SetActive(false);
        }
        
    }
}
