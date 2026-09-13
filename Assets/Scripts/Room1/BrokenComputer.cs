using UnityEngine;

public class BrokenComputer : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Broken Computer. Can't use");
    }
}
