using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SetSurvivalScreenUI : BaseScreenUI
{

    [SerializeField] private Transform survivalContainer;
    [SerializeField] private Transform survivalCraftPanelTemplate;
    [SerializeField] private Transform survivalCraftPrefab;


    



    
    private void Awake()
    {
        
        survivalCraftPanelTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        UpdateVisual(survivalContainer, survivalCraftPanelTemplate, survivalCraftPrefab,CraftingSystem.Instance.GetSurvivalBlueprintList());
        InitiateUIElements(CraftingSystem.Instance.GetSurvivalBlueprintList()[0]);
    }

    private void Update()
    {
        
        // Assign the required items amount text
        AssignUIElements(CraftingSystem.Instance.GetSurvivalBlueprintList());
    }
    
    

}
