using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class Food_Bar : MonoBehaviour
{
    // Slider component for displaying food bar
    private Slider slider;

    // Text component for displaying food counter
    public Text foodcounter;

    // Reference to the playerstate object
    public GameObject playerstate;

    // Variables to store current and maximum food values
    private float currentfood, Maxfood;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Initialize the slider component
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Retrieve current and maximum food values from playerstate
        currentfood = playerstate.GetComponent<playerstate>().currentfood;
        Maxfood = playerstate.GetComponent<playerstate>().maxfood;

        // Calculate the fill value for the slider based on current and maximum food
        float fillValue = currentfood / Maxfood;

        // Set the slider value to represent the fill value
        slider.value = fillValue;

        // Update the food counter text with current and maximum food values
        foodcounter.text = currentfood + "/" + Maxfood;
    }
}
