using UnityEngine;

public class WorkingComputer : MonoBehaviour, IInteractable
{
    [Header("UI Reference")]
    public GameObject computerScreenUI; // computer Screen panel

    public void Interact()
    {
        if (computerScreenUI != null)
        {
            // Turn on computer
            computerScreenUI.SetActive(true);
            Debug.Log("Computer Turn On!");
        }
        else
        {
            Debug.LogError("There are no panel in computer!"); 
        }
    }
}