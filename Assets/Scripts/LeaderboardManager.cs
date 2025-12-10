using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class LeaderboardManager : MonoBehaviour
{
    public Text leaderboardText;
    public int maxEntries = 10;
    
    private List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();
    
    void Start()
    {
        LoadLeaderboard();
        UpdateLeaderboardText();
    }
    
    public bool IsRecordTime(float time)
    {
        if (leaderboardEntries.Count < maxEntries)
            return true;
            
        float worstTime = leaderboardEntries.Max(e => e.time);
        return time < worstTime;
    }
    
    public void AddEntry(string playerName, float time)
    {
        leaderboardEntries.Add(new LeaderboardEntry
        { 
            playerName = playerName, 
            time = time 
        });
        
        leaderboardEntries = leaderboardEntries
            .OrderBy(e => e.time)
            .Take(maxEntries)
            .ToList();
        
        SaveLeaderboard();
        UpdateLeaderboardText();
    }
    
    void UpdateLeaderboardText()
    {
        if (leaderboardText == null) return;
        
        string text = "Таблица рекордов:\n";
        
        for (int i = 0; i < leaderboardEntries.Count; i++)
        {
            var entry = leaderboardEntries[i];
            int minutes = Mathf.FloorToInt(entry.time / 60f);
            int seconds = Mathf.FloorToInt(entry.time % 60f);
            int milliseconds = Mathf.FloorToInt((entry.time * 100f) % 100f);
            text += $"{i+1}. {entry.playerName}: {minutes:00}:{seconds:00}:{milliseconds:00}\n";
        }
        
        leaderboardText.text = text;
    }

    void SaveLeaderboard()
    {
        LeaderboardData data = new LeaderboardData 
        { 
            entries = leaderboardEntries.Select(e => new LeaderboardEntry
            {
                playerName = e.playerName,
                time = e.time
            }).ToList()
        };
        
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("Leaderboard", json);
        PlayerPrefs.Save();
    }
    
    void LoadLeaderboard()
    {
        if (PlayerPrefs.HasKey("Leaderboard"))
        {
            string json = PlayerPrefs.GetString("Leaderboard");
            LeaderboardData data = JsonUtility.FromJson<LeaderboardData>(json);
            leaderboardEntries = data.entries ?? new List<LeaderboardEntry>();
        }
    }
    
    [System.Serializable]
    private class LeaderboardData
    {
        public List<LeaderboardEntry> entries;
    }

    [System.Serializable]
    public class LeaderboardEntry
    {
        public string playerName;
        public float time;
    }
}