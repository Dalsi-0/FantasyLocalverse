using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] // �ν����Ϳ��� ���̰� �ϱ� ���� �߰�
public class MiniGameUI
{
    public TextMeshProUGUI miniGameNameText;
    public TextMeshProUGUI miniGameDescText;
    public TextMeshProUGUI miniGameLeaderboradText;
    public Button startButton;

    public void SetupUI(MiniGameDataSO minigame, System.Action onAccept)
    {
        miniGameNameText.text = minigame.gameName;
        miniGameDescText.text = minigame.description;

        List<int> scores = LeaderboardManager.Instance.GetScores(minigame.sceneType == ESceneType.MiniGameBrid);

        miniGameLeaderboradText.text = $"1st - {scores[0]}\n\n2nd - {scores[1]}\n\n3rd - {scores[2]}";

        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() => onAccept?.Invoke());
    }
}

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }

    public Transform hudUISkill;

    [SerializeField] private GameObject miniGameUIPanel; // �̴ϰ��� UI �г�
    [SerializeField] private MiniGameUI miniGameUI; // �̴ϰ��� UI ����ü
    [SerializeField] private GameObject changeClothesUIPanel; // �ǻ� ���� UI �г�
    [SerializeField] private Animator latterBoxAnimator;
    [SerializeField] private Animator FadeAnimator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        latterBoxAnimator.enabled = false;
        FadeAnimation();
        if(miniGameUIPanel !=null)  miniGameUIPanel.SetActive(false);
    }

    public void ActiveOrDisableLetterbox(bool isActive)
    {
        latterBoxAnimator.enabled = true;
        if (isActive)
        {
            latterBoxAnimator.SetBool("isActive", true);

            return;
        }
        latterBoxAnimator.SetBool("isActive", false);
    }

    public void FadeAnimation()
    {
        FadeAnimator.enabled = true;
        FadeAnimator.Play("FadeIn");
    }

    public void UpdateMiniGameUI(ESceneType miniGameKey)
    {
        MiniGameDataSO miniGames = GameManager.Instance.miniGameRepository.GetMiniGameInfo(miniGameKey);

        if (miniGames.gameName != "none")
        {
            miniGameUIPanel.SetActive(true);
            miniGameUI.SetupUI(miniGames, () => StartMiniGame(miniGames.sceneType));
        }
    }


    #region ��ư���
    void StartMiniGame(ESceneType gameType)
    {
        miniGameUIPanel.SetActive(false);
        SceneLoader.Instance.LoadScene(gameType);
    }

    public void CloseMiniGameUI()
    {
        miniGameUIPanel.SetActive(false);
    }

    public void SetActiveChangeClothesUI(bool value)
    {
        changeClothesUIPanel.SetActive(value);
    }
    #endregion

}
