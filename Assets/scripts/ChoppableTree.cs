using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class ChoppableTree1 : MonoBehaviour,IobjectInteractable
{
    // Flags indicating whether the player is in range and if the tree can be chopped
    public bool playerInRange;
    public bool canBeChopped;

    // Health variables for the tree
    public float treeMaxHealth;
    public float treeHealth;

    // Animator for tree animations
    public Animator animator;

    // Calories spent chopping wood
    public float caloriesSpentChoppingWood = 20;

    // Initialization
    private void Start()
    {
        treeHealth = treeMaxHealth;
        // Get the animator from the parent object
        animator = transform.parent.transform.parent.GetComponent<Animator>();
    }

    // Triggered when something enters the collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    // Triggered when something exits the collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    // Function called when the tree is hit
    public void GetHit()
    {
        // Trigger shake animation
        animator.SetTrigger("shake");

        // Decrease tree health and deduct calories from player's food
        treeHealth = treeHealth - 1;
        playerstate.Instance.currentfood -= caloriesSpentChoppingWood;

        // Check if the tree health is depleted
        if (treeHealth <= 0)
        {
            TreeIsDead();
        }
    }

    // Function called when the tree is dead
    void TreeIsDead()
    {
        // Get tree position
        Vector3 treePosition = transform.position;

        // Destroy the tree
        Destroy(transform.parent.transform.parent.gameObject);
        canBeChopped = false;

        // Reset selection manager
        SelectionManager.Instance.SlectedTree = null;
        SelectionManager.Instance.ChopHolder.gameObject.SetActive(false);

        // Instantiate a broken tree
        GameObject brokenTree = Instantiate(Resources.Load<GameObject>("ChoppedTree"),
        new Vector3(treePosition.x, treePosition.y + 6, treePosition.z), Quaternion.Euler(0, 0, 0));
    }

    // Update function
    private void Update()
    {
        // Update global state with tree health information if it can be chopped
        if (canBeChopped)
        {
            GlobalState.Instance.resourceHealth = treeHealth;
            GlobalState.Instance.resourceMaxHealth = treeMaxHealth;
        }
    }

    public void Interact()
    {
       

    }

    public void SetObjectRelatedUI()
    {
        // Check if the object is a choppable tree and player is in range
            if (playerInRange)
            {
                canBeChopped = true;
                SelectionManager.Instance.SlectedTree = this.gameObject;
                SelectionManager.Instance.ChopHolder.gameObject.SetActive(true);
            }
            else
            {
                // Reset the selected tree and hide chop holder if no choppable tree is found
                if (SelectionManager.Instance.SlectedTree != null)
                {
                    SelectionManager.Instance.SlectedTree.gameObject.GetComponent<ChoppableTree1>().canBeChopped = false;
                    SelectionManager.Instance.SlectedTree = null;
                    SelectionManager.Instance.ChopHolder.gameObject.SetActive(false);
                }
            }
    }
}
