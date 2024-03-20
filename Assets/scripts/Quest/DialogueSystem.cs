using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; set; }
    public TextMeshProUGUI Dialogetxt;
    public Button Option1BTN;
    public Button Option2BTN;
    public Canvas DialogeUI;
    public bool DialogeUIActive;


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
    public void OpenDialogUI()
    {
        DialogeUI.gameObject.SetActive(true);
        DialogeUIActive = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    public void CloseDialogUI()
    {
        DialogeUI.gameObject.SetActive(false);
        DialogeUIActive = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
}
