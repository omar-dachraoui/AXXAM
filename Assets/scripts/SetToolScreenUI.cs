using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SetToolScreenUI : BaseScreenUI
{

    [SerializeField] private Transform toolContainer;
    [SerializeField] private Transform toolCraftPanelTemplate;
    [SerializeField] private Transform toolCraftPrefab;



    BlueprintSO blueprintSO;


    



    
    private void Awake()
    {
        
        toolCraftPanelTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        UpdateVisual(toolContainer, toolCraftPanelTemplate, toolCraftPrefab,CraftingSystem.Instance.GetToolBlueprintList());
        blueprintSO = CraftingSystem.Instance.GetToolBlueprintList()[0];
        InitiateUIElements(blueprintSO);
        
        
        
    }

    private void Update()
    {
        AssignUIElements(CraftingSystem.Instance.GetToolBlueprintList());
    }

    
        
    
        
        
    
    
    

}