using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class hydration_bar : MonoBehaviour
{
    // Slider component for displaying hydration bar
    private Slider slider;

    // Text component for displaying hydration counter
    public Text hydrationcounter;

    // Reference to the playerstate object
    public GameObject playerstate;

    // Variables to store current and maximum hydration values
    private float currenthydration, maxhydration;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Initialize the slider component
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Retrieve current and maximum hydration values from playerstate
        currenthydration = playerstate.GetComponent<playerstate>().currenthydration;
        maxhydration = playerstate.GetComponent<playerstate>().maxhydration;

        // Calculate the fill value for the slider based on current and maximum hydration
        float fillValue = currenthydration / maxhydration;

        // Set the slider value to represent the fill value
        slider.value = fillValue;

        // Update the hydration counter text with current hydration value
        hydrationcounter.text = currenthydration + "%";
    }
}
