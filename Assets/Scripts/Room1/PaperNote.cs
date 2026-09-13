using UnityEngine;

public class PaperNote : MonoBehaviour, IInteractable
{
    [Header("Clue Settings")]
    public string secretCode = "2005";

    public void Interact()
    {
        // Tạm thời in ra log. Nếu bạn có hệ thống UI thông báo, hãy gọi nó ở đây.
        Debug.Log($"You have read the paper: The Code is: {secretCode}");
    }
}
