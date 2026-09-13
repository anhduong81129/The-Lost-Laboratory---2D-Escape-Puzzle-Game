using UnityEngine;
using TMPro;

public class GateKeypad : MonoBehaviour, IInteractable
{
    [Header("Gate Settings")]
    public GameObject blockingObstacle; 

    [Header("Keypad UI References")]
    public GameObject keypadPanel;       // UI Keypad
    public TMP_InputField pinInput;      
    public TextMeshProUGUI statusText;   

    [Header("Security")]
    private string correctPIN = "2005";  

    public void Interact()
    {
        // Check if computer unlock the keypad
        if (ComputerSystem.isGateUnlocked)
        {
            keypadPanel.SetActive(true);
            pinInput.text = ""; 
            statusText.text = "Enter 4-digit PIN:";
        }
        else
        {
            // check if computer have not unlocked 
            Debug.Log("Can't use. System Locked!");
        }
    }

    public void SubmitPIN()
    {
        if (pinInput.text == correctPIN)
        {
            Debug.Log("Access Granted! Gate has opened.");
            
            if (blockingObstacle != null)
            {
                blockingObstacle.SetActive(false);
            }
            
            keypadPanel.SetActive(false);
        }
        else
        {
            statusText.text = "WRONG PIN!";
            pinInput.text = ""; 
        }
    }

    // X to close
    public void CloseKeypad()
    {
        keypadPanel.SetActive(false);
    }
}