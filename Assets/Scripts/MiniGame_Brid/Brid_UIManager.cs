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

    public void SetResultValue(int _bestScore, int _gameScore)
    {
        resultBestScore.text = _bestScore.ToString();
        resultGameScore.text = _gameScore.ToString();
    }

    public void ReturnToVillageButton()
    {
        Time.timeScale = 1;
    }
}
