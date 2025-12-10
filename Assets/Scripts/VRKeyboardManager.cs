using UnityEngine;
using UnityEngine.UI;

public class VRKeyboardManager : MonoBehaviour
{
    public GameObject keyboard;
    public Text inputDisplay;
    private string currentText = "";
    public int maxLength = 20;
    
    private float recordedTime;

    void Start()
    {
        inputDisplay.text = "_";
        HideKeyboard();
    }
   
    public void AddCharacter(string character)
    {
        if (currentText.Length < maxLength)
        {
            currentText += character;
            inputDisplay.text = currentText + "_";
        }
    }
    
    public void Backspace()
    {
        if (currentText.Length > 0)
        {
            currentText = currentText.Substring(0, currentText.Length - 1);
            inputDisplay.text = currentText + "_";
        }
    }
    
    public void Submit()
    {
        if (!string.IsNullOrEmpty(currentText))
        {
            Debug.Log($"Имя '{currentText}' сохранено с временем {recordedTime:F2} сек.");
            
            LeaderboardManager leaderboard = FindObjectOfType<LeaderboardManager>();
            if (leaderboard != null)
            {
                leaderboard.AddEntry(currentText, recordedTime);
            }
            
            HideKeyboard();
            
            currentText = "";
            inputDisplay.text = "_";
        }
    }

    public void SetRecordTime(float time)
    {
        recordedTime = time;
    }
    
    public void ShowKeyboard()
    {
        keyboard.SetActive(true);
        currentText = "";
        inputDisplay.text = "_";
    }
    
    public void HideKeyboard()
    {
        keyboard.SetActive(false);
    }
}