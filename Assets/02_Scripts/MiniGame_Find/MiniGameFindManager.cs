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

    /// <summary>
    /// 게임 상태를 변경하는 함수
    /// </summary>
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

    /// <summary>
    /// 게임 시작 전 3초 카운트다운 진행
    /// </summary>
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
    
    /// <summary>
    /// 아이템(정답, 가짜) 스폰
    /// </summary>
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

    /// <summary>
    /// 새로운 최고 점수를 등록하고 리더보드를 업데이트하는 함수
    /// </summary>
    private int RegisterNewScore()
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

    /// <summary>
    /// 결과 UI에 최고 점수 및 현재 점수를 표시
    /// </summary>
    public void SetResultValue(float bestScore, float gameScore)
    {
        resultBestScore.text = bestScore.ToString("F0");
        resultGameScore.text = gameScore.ToString("F0");
    }

    /// <summary>
    /// 마을로 돌아가는 버튼 기능
    /// </summary>
    public void ReturnToVillageButton()
    {
        resultUI.SetActive(false);
        SceneLoader.Instance.LoadScene(ESceneType.Village);
        Time.timeScale = 1;
    }
}
