using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EMiniGameBridState
{
    Ready,
    Playing,
    GameOver,
}

public class Brid_GameManager : MonoBehaviour
{
    EMiniGameBridState gameState = EMiniGameBridState.Ready;
    public GameObject[] grounds;
    public GameObject[] obstacles;
    public GameObject obstaclesParent;
    public BridController bridController;
    public Brid_UIManager brid_UIManager;

    public int gameScore;

    private void Start()
    {
        ChangeSystemState(EMiniGameBridState.Ready);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (gameState == EMiniGameBridState.Ready)
            {
                ChangeSystemState(EMiniGameBridState.Playing);
            }
            else if (gameState == EMiniGameBridState.Playing)
            {
                bridController.Jump();
            }
        }
    }

    /// <summary>
    /// 게임 상태를 변경하는 함수
    /// </summary>
    public void ChangeSystemState(EMiniGameBridState state)
    {
        gameState = state;
        switch (gameState)
        {
            case EMiniGameBridState.Ready:
                EnterStageReady();
                break;

            case EMiniGameBridState.Playing:
                EnterStagePlaying();
                break;

            case EMiniGameBridState.GameOver:
                EnterStageGameOver();
                break;
        }
    }

    private void EnterStageReady()
    {
        bridController.SetRigidbodyGravityScale(0);
        obstaclesParent.SetActive(false);
        gameScore = 0;
    }

    private void EnterStagePlaying()
    {
        bridController.SetRigidbodyGravityScale(0.5f);
        obstaclesParent.SetActive(true);
        brid_UIManager.SetActive_pressAnyKeyUI(false);
        brid_UIManager.SetActive_playingUI(true);
        brid_UIManager.SetActive_resultUI(false);
    }

    private void EnterStageGameOver()
    {
        Time.timeScale = 0;
        brid_UIManager.SetActive_pressAnyKeyUI(false);
        brid_UIManager.SetActive_playingUI(false);
        brid_UIManager.SetActive_resultUI(true);

        bridController.gameObject.SetActive(false);

        brid_UIManager.SetResultValue(RegisterNewScore(), gameScore);
    }

    /// <summary>
    /// 새로운 점수를 등록하고 리더보드를 업데이트하는 함수
    /// </summary>
    private int RegisterNewScore()
    {
        List<int> ranks = LeaderboardManager.Instance.GetScores(true);

        ranks.Add(gameScore);

        int[] array = ranks.ToArray();
        Array.Sort(array);
        Array.Reverse(array);
        ranks = array.ToList<int>();

        ranks = ranks.GetRange(0, 3);

        LeaderboardManager.Instance.UpdateLeaderboard(ranks, true);

        return ranks[0];
    }

    public void PlusScore()
    {
        gameScore++;
        brid_UIManager.SetPlayingGameScoreText(gameScore);
    }

    public EMiniGameBridState GetGameState()
    {
        return gameState;
    }
}
