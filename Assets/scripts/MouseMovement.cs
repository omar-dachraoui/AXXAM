using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    // Sensitivity of mouse movement
    public float mouseSensitivity = 100f;

    // Rotation around the X and Y axes
    float xRotation = 0f;
    float YRotation = 0f;

    void Start()
    {
        // Locking the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Allowing mouse movement only if neither the inventory nor crafting screens are open
        if (!InventorySystem.Instance.isOpen && !CraftingSystem.Instance.isOpen)
        {
            // Get mouse input for rotation
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Control rotation around x axis (Look up and down)
            xRotation -= mouseY;

            // Clamp the rotation to prevent over-rotation
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Control rotation around y axis (Look left and right)
            YRotation += mouseX;

            // Applying both rotations
            transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);
        }
    }
}
