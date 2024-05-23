using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Text = TMPro.TextMeshProUGUI;
using UnityEngine.UI;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;

public class SelectionManager : MonoBehaviour
{
    // Reference to the UI element for displaying interaction information
    public GameObject interaction_Info_UI;
    public Text interaction_text;
    public bool ontarget;
    public GameObject SelectedObject;
    public GameObject SlectedTree;
    public GameObject ChopHolder;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    
    public static SelectionManager Instance { get; set; }

    // Reference to the center dot and hand icons
    
    public Image HandIcon;

    // Flag to track if the hand icon is visible
    public bool HandIsVisible;

  

    private void Awake()
    {
        // Singleton implementation: destroy duplicate instances
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
         // Initialize the interaction_text by getting the Text component from interaction_Info_UI
        interaction_text = interaction_Info_UI.GetComponent<Text>();
        ontarget = false;
    }

    void Update()
    {
        // Cast a ray from the main camera to the mouse position on the screen
       // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        float maxDistance = 8f;
        
        // Check if the ray hits any object in the scene
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward,out RaycastHit hit,maxDistance,pickUpLayerMask))
        {

            
            
            // Get the transform of the object hit by the ray
            var selectionTransform = hit.transform;

            InteractableObject HasInetractableScript = selectionTransform.GetComponent<InteractableObject>();

            IobjectInteractable Has = selectionTransform.GetComponent<IobjectInteractable>();

            ChoppableTree1 choppableTree = selectionTransform.GetComponent<ChoppableTree1>();

            NPCInteraction nPCInteraction = selectionTransform.GetComponent<NPCInteraction>();
              

            if(Has != null)
            {
               Has.SetObjectRelatedUI();
                


                if(Input.GetKeyDown(KeyCode.E))
                {
                    Has.Interact();
                }
            }
            


            
           
        }
        else
        {
            ontarget = false;
            // Hide the interaction UI if no object is hit by the ray
            interaction_Info_UI.SetActive(false);
            HandIcon.gameObject.SetActive(false);
            
            HandIsVisible = false;
            ChopHolder.gameObject.SetActive(false);
        }
    }

    // Enable selection UI elements
    public void enableselction()
    {
        HandIcon.enabled = true;
        
        interaction_Info_UI.SetActive(true);
    }

    // Disable selection UI elements
    public void disableselction()
    {
        HandIcon.enabled = false;
        
        interaction_Info_UI.SetActive(false);
        
        SelectedObject = null;
    }

    
}
