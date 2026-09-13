using UnityEngine;
using TMPro;
using System.Collections;

public class ComputerSystem : MonoBehaviour
{
    [Header("Player Control")]
    // Kéo script di chuyển của Player vào đây
    public MonoBehaviour playerMovementScript; 

    [Header("UI Panels")]
    public GameObject documentsPanel;
    public GameObject calendarPanel;
    public GameObject passwordPanel;
    
    [Header("UI Elements")]
    public TextMeshProUGUI notificationText;
    public TMP_InputField passwordInput;

    // BIẾN QUAN TRỌNG: Lưu trạng thái xem password đã giải mã chưa
    // Dùng 'static' để các script khác (như Keypad) có thể đọc được dễ dàng
    public static bool isGateUnlocked = false; 
    private string correctPassword = "2026TheLostLab"; // Mật khẩu nằm trong file Door_Access

    void OnEnable()
    {
        // Gọi khi màn hình máy tính được bật lên (WorkingComputer gọi SetActive(true))
        CloseAllApps();
        notificationText.text = "";
    }

    public void CloseComputer()
    {
        gameObject.SetActive(false); // Tắt màn hình
    }

    // Tắt các cửa sổ đang mở
    public void CloseAllApps()
    {
        documentsPanel.SetActive(false);
        calendarPanel.SetActive(false);
        passwordPanel.SetActive(false);
    }

    // --- CÁC HÀM CHO APP ICON ---
    public void OnClickDocuments()
    {
        CloseAllApps();
        documentsPanel.SetActive(true);
    }

    public void OnClickCalendar()
    {
        CloseAllApps();
        calendarPanel.SetActive(true);
    }

    public void OnClickProtection()
    {
        StartCoroutine(ShowMessageRoutine("Computer Save"));
    }

    public void OnClickNetwork()
    {
        StartCoroutine(ShowMessageRoutine("no signal"));
    }

    public void OnClickGateDoorEXE()
    {
        CloseAllApps();
        passwordPanel.SetActive(true);
    }

    // --- LOGIC COROUTINE & PASSWORD ---
    private IEnumerator ShowMessageRoutine(string msg)
    {
        notificationText.text = msg;
        yield return new WaitForSeconds(1.5f);
        notificationText.text = "";
    }

    // Gắn hàm này vào sự kiện OnClick của nút Submit trong Password Panel
    public void SubmitPassword()
    {
        if (passwordInput.text == correctPassword)
        {
            isGateUnlocked = true;
            StartCoroutine(ShowMessageRoutine("ACCESS GRANTED!\nKeypad Unlocked."));
            passwordPanel.SetActive(false);
        }
        else
        {
            StartCoroutine(ShowMessageRoutine("WRONG PASSWORD!"));
        }
    }
}