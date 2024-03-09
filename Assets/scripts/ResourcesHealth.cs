using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesHealth : MonoBehaviour
{
    // Reference to the slider component
    private Slider slider;

    // Variables to store current and maximum health values
    private float currenthealth, maxhealth;

    // Reference to the GlobalState object
    public GameObject GlobalState;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get the Slider component attached to this GameObject
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Retrieve the current and maximum health values from the GlobalState object
        currenthealth = GlobalState.GetComponent<GlobalState>().resourceHealth;
        maxhealth = GlobalState.GetComponent<GlobalState>().resourceMaxHealth;

        // Calculate the fill value for the slider based on the current and maximum health
        float fillValue = currenthealth / maxhealth;

        // Set the slider value to the calculated fill value
        slider.value = fillValue;
    }
}
