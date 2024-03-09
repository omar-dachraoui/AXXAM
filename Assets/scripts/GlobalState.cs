using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : MonoBehaviour
{
    // Singleton instance of GlobalState
    public static GlobalState Instance { get; set; }

    // Variables to store resource health and maximum health
    public float resourceHealth;
    public float resourceMaxHealth;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (Instance != null && Instance != this)
        {
            // Destroy the duplicate instance if it exists
            Destroy(gameObject);
        }
        else
        {
            // Set this instance as the singleton instance
            Instance = this;
        }
    }
}
