using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolScreenUI : MonoBehaviour
{
   [SerializeField] private Transform container;
    [SerializeField] private Transform craftPanelTemplate;

    private void Awake()
    {
       craftPanelTemplate.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        
        {
            UpdateVisual();
        }
        
    }
   
    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if(child == craftPanelTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);
        }

        foreach(BlueprintSO blueprintSO in CraftingSystem.Instance.GetToolBlueprintList())
        {
           Transform craftPanelTransform = Instantiate(craftPanelTemplate, container);
            craftPanelTransform.gameObject.SetActive(true);
        }




    }
}
