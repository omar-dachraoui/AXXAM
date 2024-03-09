using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    // Components for drag-and-drop functionality
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    // Static variable to keep track of the item being dragged
    public static GameObject itemBeingDragged;

    // Variables to store the starting position and parent of the dragged item
    Vector3 startPosition;
    Transform startParent;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get the required components
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Called on the start of a drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        // Adjust the transparency during drag
        canvasGroup.alpha = .6f;
        // Set blocksRaycasts to false so that the raycast ignores the dragged item
        canvasGroup.blocksRaycasts = false;
        // Save the starting position and parent
        startPosition = transform.position;
        startParent = transform.parent;
        // Set the item as a child of the root to prevent it from being blocked by other UI elements
        transform.SetParent(transform.root);
        // Set the static variable to the current dragged item
        itemBeingDragged = gameObject;
    }

    // Called while dragging
    public void OnDrag(PointerEventData eventData)
    {
        // Move the item with the mouse, considering canvas scale
        rectTransform.anchoredPosition += eventData.delta;
    }

    // Called at the end of a drag
    public void OnEndDrag(PointerEventData eventData)
    {
        // Clear the static variable as the drag ends
        itemBeingDragged = null;

        // Return the item to its original position if dropped back to the original parent or outside any valid parent
        if (transform.parent == startParent || transform.parent == transform.root)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }

        Debug.Log("OnEndDrag");
        // Reset transparency and enable raycasts
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
