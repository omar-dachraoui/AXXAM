using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BaseScreenUI : MonoBehaviour
{

    
    
    
   
    [HideInInspector] public List<string> InventoryItemList = new List<string>();
    
    List<TextMeshProUGUI> textReqList = new List<TextMeshProUGUI>(3);
    List<Transform> craftPrefabTransformList= new List<Transform>(3);

    List<int> ReqAmountList=new List<int>(3);
    List<int> ReqCountList = new List<int>(3);

    
    




    private Sprite image;

    private Button craftBTN;

    Transform craftTransform,craftPanelTransform;

    public static BaseScreenUI Instance { get; set; }



    
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




    public virtual void UpdateVisual(Transform container, Transform craftPanelTemplate, Transform craftPrefab,List<BlueprintSO> blueprintSOList)
    {

        
        
        
        
       if(container != null)
        {
            foreach (Transform child in container)
            {
                if(child == craftPanelTemplate)
                {
                    continue;
                }
                Destroy(child.gameObject);
            }
        }

        

        foreach(Transform child in craftPanelTemplate)
        {
            if(child == craftPrefab)
            {
                child.gameObject.SetActive(false);
                continue;
            }
            Destroy(child.gameObject);
        }





        
        SpawnPrefabForEachPanel(blueprintSOList,container,craftPanelTemplate,craftPrefab);
               
       

    }


    private void SpawnPrefabForEachPanel(List<BlueprintSO> blueprintSOList,Transform container, Transform craftPanelTemplate, Transform craftPrefab)
    {
        
        List<Transform> craftPanelTransformList = new List<Transform>();
        
        for(int i = 0; i < blueprintSOList.Count; i+=3)
        {
            
            craftPanelTransform = Instantiate(craftPanelTemplate, container);
            craftPanelTransform.gameObject.SetActive(true);
            craftPanelTransformList.Add(craftPanelTransform);
 

        }
        int k = blueprintSOList.Count;
        for(int i = 0; i< craftPanelTransformList.Count; i++)
        {
            
                
            for(int j = 0; j < k ; j++)
            {
                if(j == 3)
                {
                    k -= 3;
                    
                    break;
                }
                craftTransform = Instantiate(craftPrefab, craftPanelTransformList[i]);
                craftTransform.gameObject.SetActive(true);
                craftPrefabTransformList.Add(craftTransform);
                
            }
        }
    }






    public void InitiateUIElements(BlueprintSO blueprintSO)
    {

        // Assign the image of the crafted item
        UnityEngine.UI.Image test = craftTransform.transform.Find("Image").GetComponent<UnityEngine.UI.Image>();
        image = blueprintSO.image;
        test.sprite = image;

        // Assign the crafting button
        craftBTN = craftTransform.transform.Find("Button").GetComponent<Button>();
        craftBTN.onClick.AddListener(delegate { CraftingSystem.Instance.CraftAnyItem(blueprintSO); });    

       
        // Assign the required items text
        textReqList.Add(craftTransform.transform.Find("req1").GetComponent<TextMeshProUGUI>());
        textReqList.Add(craftTransform.transform.Find("req2").GetComponent<TextMeshProUGUI>());
        textReqList.Add(craftTransform.transform.Find("req3").GetComponent<TextMeshProUGUI>());

        // Reset the text
        textReqList[0].text="";
        textReqList[1].text="";
        textReqList[2].text="";
        // Reset the list
        ReqCountList.Add(0);
        ReqCountList.Add(0);
        ReqCountList.Add(0);
        

        

        



        
        
    }



    public void AssignUIElements(List<BlueprintSO> blueprintSOList)
    {
        
        CycleThroughtBPList(blueprintSOList);
        EnableDisableCraftBTN(blueprintSOList);
       

    }

   





    private void CycleThroughtBPList(List<BlueprintSO> blueprintSOList)
    {
        
        foreach(  BlueprintSO blueprintSO in blueprintSOList)
        {

            
            
            
            image = blueprintSO.image;

            for(int i = 0; i < blueprintSO.ReqList.Count; i++)
            {
                textReqList[i].text = blueprintSO.ReqList[i];    

                ReqAmountList.Add ( blueprintSO.ReqAmountList[i]);

                
                    
            }
        }

      
    }


    private void EnableDisableCraftBTN( List<BlueprintSO> blueprintSOList)
    {
         
       RefrechNeededItem();

        
        foreach(  BlueprintSO blueprintSO in blueprintSOList)
        {
            
            
           
            for(int i = 0; i < blueprintSO.ReqList.Count; i++)
            {
               
               textReqList[i].text = ReqAmountList[i]   + textReqList[i].text + "[" + ReqCountList[i]+ "]";

                switch (blueprintSO.ReqList.Count)
                {
                    case 1:
                        if ( ReqCountList[0] >= blueprintSO.ReqAmountList[0]    &&   InventorySystem.Instance.CheckSlotAvailable(1))
                        {
                            craftBTN.gameObject.SetActive(true);
                        }
                        else
                        {
                            craftBTN.gameObject.SetActive(false);
                        }
                        break;
                    case 2:
                        if ( ReqCountList[0] >= blueprintSO.ReqAmountList[0] && ReqCountList[1] >= blueprintSO.ReqAmountList[1]   &&   InventorySystem.Instance.CheckSlotAvailable(1))
                        {
                            craftBTN.gameObject.SetActive(true);
                        }
                        else
                        {
                            craftBTN.gameObject.SetActive(false);
                        }
                        break;
                    case 3:
                        if ( ReqCountList[0] >= blueprintSO.ReqAmountList[0] && ReqCountList[1] >= blueprintSO.ReqAmountList[1] && ReqCountList[2] >= blueprintSO.ReqAmountList[2]   &&   InventorySystem.Instance.CheckSlotAvailable(1))
                        {
                            craftBTN.gameObject.SetActive(true);
                        }
                        else
                        {
                            craftBTN.gameObject.SetActive(false);
                        }
                        break;
                    
                }
                    
            }

            
        }
    }




 

    
    



    public void RefrechNeededItem()
    {

        // Reset the list
        ReqCountList[0]=0;
        ReqCountList[1]=0;
        ReqCountList[2]=0;
        // Get the list of items in the inventory
        InventoryItemList = InventorySystem.Instance.itemList;
        // Get the names of the required items
        string itemReq1Name = textReqList[0].text;
        string itemReq2Name = textReqList[1].text;
        string itemReq3Name = textReqList[2].text;


        
        foreach (string itemName in InventoryItemList)
        {
            switch (itemName)
            {
                case string reqName when reqName == itemReq1Name:
                    ReqCountList[0]+=1;
                    
                break;
                case string reqName when reqName == itemReq2Name:
                    ReqCountList[1]+=1;
                break;
                case string reqName when reqName == itemReq3Name:
                    ReqCountList[2]+=1;
                break;

            }
                   
        }


       

        
       
    }

   


  


}