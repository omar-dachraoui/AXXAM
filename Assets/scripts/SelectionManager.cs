using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Text = TMPro.TextMeshProUGUI;
using UnityEngine.UI;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;

public class SelectionManager : MonoBehaviour, IobjectInteractable
{
    // Reference to the UI element for displaying interaction information
    public GameObject interaction_Info_UI;
    Text interaction_text;
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

    private void Start()
    {
        // Initialize the interaction_text by getting the Text component from interaction_Info_UI
        interaction_text = interaction_Info_UI.GetComponent<Text>();
        ontarget = false;
    }

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
            ChoppableTree1 choppableTree = selectionTransform.GetComponent<ChoppableTree1>();
            NPCInteraction nPCInteraction = selectionTransform.GetComponent<NPCInteraction>();



            if(nPCInteraction && nPCInteraction.playerInRange)
            {
                interaction_text.text = "Press (T) to Talk";
                interaction_Info_UI.SetActive(true);
                if(Input.GetKeyDown(KeyCode.T) && !nPCInteraction.isTalkingWithPlayer)
                {
                    nPCInteraction.StartConversation();
                }
            }
            else
            {
                interaction_text.text = "";
                interaction_Info_UI.SetActive(false);
            }





            // Check if the object is a choppable tree and player is in range
            if (choppableTree && choppableTree.playerInRange)
            {
                choppableTree.canBeChopped = true;
                SlectedTree = choppableTree.gameObject;
                ChopHolder.gameObject.SetActive(true);
            }
            else
            {
                // Reset the selected tree and hide chop holder if no choppable tree is found
                if (SlectedTree != null)
                {
                    SlectedTree.gameObject.GetComponent<ChoppableTree1>().canBeChopped = false;
                    SlectedTree = null;
                    ChopHolder.gameObject.SetActive(false);
                }
            }

            // Check if the object has an InteractableObject component
            if (HasInetractableScript && HasInetractableScript.playerinrange)
            {
                ontarget = true;
                SelectedObject = HasInetractableScript.gameObject;
                // Display the item name from the InteractableObject component
                interaction_text.text = HasInetractableScript.GetItemName();

                // Show the interaction UI
                interaction_Info_UI.SetActive(true);

                // Check if the object is pickable and adjust the icons accordingly
                if (HasInetractableScript.CompareTag("pickable"))
                {
                    
                    HandIcon.gameObject.SetActive(true);
                    HandIsVisible = true;
                }
                else
                {
                    HandIcon.gameObject.SetActive(false);
                    
                    HandIsVisible = false;
                }
            }
            else
            {
                ontarget = false;
                // Hide the interaction UI if no InteractableObject component is found
                //interaction_Info_UI.SetActive(false);
                HandIcon.gameObject.SetActive(false);
                
                HandIsVisible = false;
            }
        }
        else
        {
            ontarget = false;
            // Hide the interaction UI if no object is hit by the ray
            interaction_Info_UI.SetActive(false);
            HandIcon.gameObject.SetActive(false);
            
            HandIsVisible = false;
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

    public void Interact()
    {
        throw new System.NotImplementedException();
    }
}
