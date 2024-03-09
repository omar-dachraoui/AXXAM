using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Equiqable_Item : MonoBehaviour
{
    // Animator component for handling animations
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the animator component
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check conditions to trigger the "hit" animation
        if (Input.GetMouseButtonDown(0) && // Left Mouse Button
            InventorySystem.Instance.isOpen == false &&
            CraftingSystem.Instance.isOpen == false &&
            SelectionManager.Instance.HandIsVisible == false
        )
        {

            StartCoroutine(swing());
            
            

            // Trigger the "hit" animation
            animator.SetTrigger("hit");
        }
    }
    IEnumerator swing()
    {
        yield return new WaitForSeconds(0f);
        Sound_Manager.Instance.PlaySound(Sound_Manager.Instance.swingSound);
        
    }

    // Method to handle the object being hit
    public void GetHit()
    {
        // Get the selected tree from the selection manager
        GameObject selectedTree = SelectionManager.Instance.SlectedTree;

        // Check if a tree is selected and invoke its GetHit method
        if (selectedTree != null)
        {
            Sound_Manager.Instance.PlaySound(Sound_Manager.Instance.chopsound);
            selectedTree.GetComponent<ChoppableTree1>().GetHit();
            
        }
    }
}
