using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
public enum EMiniGameFindState
{
    Ready,
    Playing,
    GameOver,
}

public class MiniGameFindManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject realItem;
    [SerializeField] private GameObject fakeItem;
    [SerializeField] private GameObject readyZone;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] GameObject resultUI;
    [SerializeField] TextMeshProUGUI resultBestScore;
    [SerializeField] TextMeshProUGUI resultGameScore;

    private float timer = 0f;
    private EMiniGameFindState gameState = EMiniGameFindState.Ready;

    private void Start()
    {
        ChangeGameState(EMiniGameFindState.Ready);
    }

    private void Update()
    {
        if (gameState == EMiniGameFindState.Playing)
        {
            timer += Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F0");
        }
    }

    public void ChangeGameState(EMiniGameFindState state)
    {
        gameState = state;
        switch (gameState)
        {
            case EMiniGameFindState.Ready:
                readyZone.SetActive(true);
                resultUI.SetActive(false);
                timerText.gameObject.SetActive(false);
                StartCoroutine(StartCountdown());
                break;

            case EMiniGameFindState.Playing:
                timer = 0f;
                readyZone.SetActive(false);
                timerText.gameObject.SetActive(true);
                break;

            case EMiniGameFindState.GameOver:
                Time.timeScale = 0f;
                resultUI.SetActive(true);
                SetResultValue(RegisterNewScore(), timer);
                break;
        }
    }

    private IEnumerator StartCountdown()
    {
        countdownText.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        countdownText.gameObject.SetActive(false);
        SpawnItems();
        ChangeGameState(EMiniGameFindState.Playing);
    }

    private void SpawnItems()
    {
        if (spawnPoints.Length == 0) return;

        int realItemIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        Instantiate(realItem, spawnPoints[realItemIndex].position, Quaternion.identity);

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i == realItemIndex) continue;
            GameObject fake = Instantiate(fakeItem, spawnPoints[i].position, Quaternion.identity);
        }
    }

    public void EndGame()
    {
        ChangeGameState(EMiniGameFindState.GameOver);
    }

    int RegisterNewScore()
    {
        List<int> ranks = LeaderboardManager.Instance.GetScores(false);

        // 0을 제외한 점수만 리스트에 저장
        ranks = ranks.Where(score => score != 0).ToList();
        ranks.Add((int)timer);
        ranks.Sort();
        while (ranks.Count < 3)
        {
            ranks.Add(0);
        }

        ranks = ranks.Take(3).ToList();
        LeaderboardManager.Instance.UpdateLeaderboard(ranks, false);

        return ranks[0];
    }

    public void SetResultValue(float bestScore, float gameScore)
    {
        resultBestScore.text = bestScore.ToString("F0");
        resultGameScore.text = gameScore.ToString("F0");
    }

    // 버튼기능
    public void ReturnToVillageButton()
    {
        resultUI.SetActive(false);
        SceneLoader.Instance.LoadScene(ESceneType.Village);
        Time.timeScale = 1;
    }
}
