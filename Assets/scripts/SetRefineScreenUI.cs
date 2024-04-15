using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SetRefineScreenUI : BaseScreenUI
{

    [SerializeField] private Transform refineContainer;
    [SerializeField] private Transform refineCraftPanelTemplate;
    [SerializeField] private Transform refineCraftPrefab;


    



    
    private void Awake()
    {
        
        refineCraftPanelTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        UpdateVisual(refineContainer, refineCraftPanelTemplate, refineCraftPrefab,CraftingSystem.Instance.GetRefineBlueprintList());
         InitiateUIElements(CraftingSystem.Instance.GetRefineBlueprintList()[0]);
    }

    private void Update()
    {
       
        // Assign the required items amount text
        AssignUIElements(CraftingSystem.Instance.GetRefineBlueprintList());
    }
    
    

}