using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerstate : MonoBehaviour
{
    // Singleton pattern to ensure only one instance of the playerstate exists
    public static playerstate Instance { get; set; }

    // Player health
    public float currenthealth;
    public float maxhealth;

    // Player calories
    public float currentfood;
    public float maxfood;
    float distancetravelled = 0;
    Vector3 lastposition;
    public GameObject player;

    // Player hydration
    public float currenthydration;
    public float maxhydration;
    public bool hydrationTrigger;

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

    private void Start()
    {
        // Initialize player health, calories, and hydration
        currenthealth = maxhealth;
        currentfood = maxfood;
        StartCoroutine(decreasehydration());
        currenthydration = maxhydration;
    }

    // Coroutine to simulate a decrease in hydration over time
    IEnumerator decreasehydration()
    {
        while (true)
        {
            currenthydration -= 1;
            yield return new WaitForSeconds(2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Track the distance travelled by the player
        distancetravelled += Vector3.Distance(player.transform.position, lastposition);
        lastposition = player.transform.position;

        // Decrease food based on distance travelled
        if (distancetravelled >= 5)
        {
            distancetravelled = 0;
            currentfood -= 1;
        }

        // Testing health slider
        if (Input.GetKeyDown(KeyCode.D))
        {
            currenthealth -= 10;
        }
    }

    // Method to set player health
    public void setHealth(float newHealth)
    {
        currenthealth = newHealth;
    }

    // Method to set player food level
    public void setfood(float newfood)
    {
        currentfood = newfood;
    }

    // Method to set player hydration level
    public void setHydration(float newHydration)
    {
        currenthydration = newHydration;
    }
}
