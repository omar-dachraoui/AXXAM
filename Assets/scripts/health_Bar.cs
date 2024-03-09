using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class health_Bar : MonoBehaviour
{
    // Slider component for displaying health bar
    private Slider slider;

    // Text component for displaying health counter
    public Text healthcounter;

    // Reference to the playerstate object
    public GameObject playerstate;

    // Variables to store current and maximum health values
    private float currenthealth, MaxHealth;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Initialize the slider component
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Retrieve current and maximum health values from playerstate
        currenthealth = playerstate.GetComponent<playerstate>().currenthealth;
        MaxHealth = playerstate.GetComponent<playerstate>().maxhealth;

        // Calculate the fill value for the slider based on current and maximum health
        float fillValue = currenthealth / MaxHealth;

        // Set the slider value to represent the fill value
        slider.value = fillValue;

        // Update the health counter text with current and maximum health values
        healthcounter.text = currenthealth + "/" + MaxHealth;
    }
}
