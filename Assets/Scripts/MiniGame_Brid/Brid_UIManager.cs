using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Brid_UIManager : MonoBehaviour
{
    [SerializeField] GameObject pressAnyKeyUI;
    [SerializeField] GameObject playingUI;
    [SerializeField] GameObject resultUI;

    [SerializeField] TextMeshProUGUI playingGameScore;
    [SerializeField] TextMeshProUGUI resultBestScore;
    [SerializeField] TextMeshProUGUI resultGameScore;


    public void SetPlayingGameScoreText(int score)
    {
        playingGameScore.text = score.ToString();
    }

    public void SetActive_pressAnyKeyUI(bool active)
    {
        pressAnyKeyUI.SetActive(active);
    }
    public void SetActive_playingUI(bool active)
    {
        playingUI.SetActive(active);
    }
    public void SetActive_resultUI(bool active)
    {
        resultUI.SetActive(active);
    }

    public void SetResultValue(int bestScore, int gameScore)
    {
        resultBestScore.text = bestScore.ToString();
        resultGameScore.text = gameScore.ToString();
    }

    // ��ư���
    public void ReturnToVillageButton()
    {
        resultUI.SetActive(false);
        SceneLoader.Instance.LoadScene(ESceneType.Village);
        Time.timeScale = 1;
    }
}
