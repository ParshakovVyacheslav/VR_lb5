using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AssemblyTimerManager : MonoBehaviour
{
    public Text timerText;

    public bool isDone = false;
    public bool isAssembling = true;
    public int currentDetailsCount = 0;

    private bool timerIsRunning = false;
    private float elapsedTime = 0f;

    public static AssemblyTimerManager Instance;
    public VRKeyboardManager keyboardManager;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timerText.text = "Time\n00:00:00";
    }

    void Update()
    {
        if (timerIsRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay(elapsedTime);
        }
    }

    public void StartTimer()
    {
        if (!timerIsRunning)
        {
            timerIsRunning = true;
            elapsedTime = 0f;
            isDone = false;
        }
    }

    public void StopTimer()
    {
        if (timerIsRunning)
        {
            timerIsRunning = false;
        }
    }

    void UpdateTimerDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 100f) % 100f);

        timerText.text = string.Format("Time\n{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    public void SetAssemblyState(bool assembled)
    {
        isDone = assembled;
    }

    public void SetRecord()
    {
        LeaderboardManager leaderboard = FindObjectOfType<LeaderboardManager>();
    
        if (elapsedTime <= 60f && leaderboard != null && leaderboard.IsRecordTime(elapsedTime))
        {
            keyboardManager.SetRecordTime(elapsedTime);
            keyboardManager.ShowKeyboard();
        }
    }

    public void AddDetail() 
    {
        StartTimer();
        currentDetailsCount++;
        if (currentDetailsCount == 6)
        {
            isAssembling = false;
        }
    }
    public void RemoveDetail() 
    {
        currentDetailsCount--;
        if (!isAssembling && currentDetailsCount == 0)
        {
            isAssembling = true;
            isDone = true;
        }
    }
}