using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameState
{
    Ready,
    Playing,
    GameOver,
}

public class Brid_GameManager : MonoBehaviour
{
    EGameState gameState = EGameState.Ready;
    public GameObject[] grounds;
    public GameObject[] obstacles;
    public GameObject obstaclesParent;
    public BridController bridController;
    public Brid_UIManager brid_UIManager;

    public int gameScore;

    private void Start()
    {
        ChangeSystemState(EGameState.Ready);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (gameState == EGameState.Ready)
            {
                ChangeSystemState(EGameState.Playing);
            }
            else if (gameState == EGameState.Playing)
            {
                bridController.Jump();
            }
        }
    }

    public void ChangeSystemState(EGameState state)
    {
        gameState = state;
        switch (gameState)
        {
            case EGameState.Ready:
                EnterStageReady();
                break;

            case EGameState.Playing:
                EnterStagePlaying();
                break;

            case EGameState.GameOver:
                EnterStageGameOver();
                break;
        }
    }

    void EnterStageReady()
    {
        bridController.SetRigidbodyGravityScale(0);
        obstaclesParent.SetActive(false);
        gameScore = 0;
    }

    void EnterStagePlaying()
    {
        bridController.SetRigidbodyGravityScale(0.5f);
        obstaclesParent.SetActive(true);
        brid_UIManager.SetActive_pressAnyKeyUI(false);
        brid_UIManager.SetActive_playingUI(true);
        brid_UIManager.SetActive_resultUI(false);
    }

    void EnterStageGameOver()
    {
        Time.timeScale = 0;
        brid_UIManager.SetActive_pressAnyKeyUI(false);
        brid_UIManager.SetActive_playingUI(false);
        brid_UIManager.SetActive_resultUI(true);
        brid_UIManager.SetResultValue(0, gameScore);
    }

    public void PlusScore()
    {
        gameScore++;
        brid_UIManager.SetPlayingGameScoreText(gameScore);
    }

}
