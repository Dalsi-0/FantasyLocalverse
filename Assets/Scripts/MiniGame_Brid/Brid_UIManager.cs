using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Brid_UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pressAnyKeyUI;
    [SerializeField] private GameObject playingUI;
    [SerializeField] private GameObject resultUI;

    [SerializeField] private TextMeshProUGUI playingGameScore;
    [SerializeField] private TextMeshProUGUI resultBestScore;
    [SerializeField] private TextMeshProUGUI resultGameScore;

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

    /// <summary>
    /// 결과 UI에서 마을로 돌아가는 버튼 기능 함수
    /// </summary>
    public void ReturnToVillageButton()
    {
        resultUI.SetActive(false);
        SceneLoader.Instance.LoadScene(ESceneType.Village);
        Time.timeScale = 1;
    }
}
