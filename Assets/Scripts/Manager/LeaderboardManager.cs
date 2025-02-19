using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance { get; private set; }

    private List<int> birdGameScores = new List<int>();
    private List<int> findGameScores = new List<int>();

    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬이 변경되어도 유지
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public List<int> GetScores(bool isBird)
    {
        LoadMiniGameScore();

        return isBird ? birdGameScores : findGameScores;
    }

    private void LoadMiniGameScore()
    {
        // brid
        birdGameScores.Clear();
        birdGameScores.Add(PlayerPrefs.GetInt("Bird_1st", 0));
        birdGameScores.Add(PlayerPrefs.GetInt("Bird_2nd", 0));
        birdGameScores.Add(PlayerPrefs.GetInt("Bird_3rd", 0));

        // find
        findGameScores.Clear();
        findGameScores.Add(PlayerPrefs.GetInt("Find_1st", 0));
        findGameScores.Add(PlayerPrefs.GetInt("Find_2nd", 0));
        findGameScores.Add(PlayerPrefs.GetInt("Find_3rd", 0));
    }

    public void UpdateLeaderboard(List<int> scores, bool isBrid)
    {
        if (isBrid)
        {
            PlayerPrefs.SetInt("Bird_1st", scores[0]);
            PlayerPrefs.SetInt("Bird_2nd", scores[1]);
            PlayerPrefs.SetInt("Bird_3rd", scores[2]);
        }
        else
        {
            PlayerPrefs.SetInt("Find_1st", scores[0]);
            PlayerPrefs.SetInt("Find_2nd", scores[1]);
            PlayerPrefs.SetInt("Find_3rd", scores[2]);
        }
        PlayerPrefs.Save();
    }
}
