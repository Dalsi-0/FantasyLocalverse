using System.Collections;
using System.Collections.Generic;
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

        bridController.gameObject.SetActive(false);
        int highScore = PlayerPrefs.GetInt("BestScore", 0);
        if(highScore < gameScore)
        {
            highScore = gameScore;
        }

        brid_UIManager.SetResultValue(highScore, gameScore);
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
