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

    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private RectTransform dialoguePanel;
    [SerializeField] private GameObject miniGameUIPanel; // �̴ϰ��� UI �г�
    [SerializeField] private MiniGameUI miniGameUI; // �̴ϰ��� UI ����ü
    [SerializeField] private GameObject changeClothesUIPanel; // �ǻ� ���� UI �г�
    [SerializeField] private Animator latterBoxAnimator;
    [SerializeField] private Animator FadeAnimator;
    public ClothesController clothesController;

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

    private void Init()
    {
        // ���� Canvas ũ�� ��������
        RectTransform canvasRect = mainCanvas.GetComponent<RectTransform>();
        float canvasWidth = canvasRect.rect.width;


        dialoguePanel.sizeDelta = new Vector2(canvasWidth, dialoguePanel.sizeDelta.y);
        for (int i = 0; i < 2; i++)
        {
            RectTransform rectLatterBox = latterBoxAnimator.transform.GetChild(i).GetComponent<RectTransform>();
            rectLatterBox.sizeDelta = new Vector2(canvasWidth, rectLatterBox.sizeDelta.y);
        }
        RectTransform rectFade = FadeAnimator.transform.GetComponent<RectTransform>();
        rectFade.sizeDelta = new Vector2(canvasWidth, rectFade.sizeDelta.y);


        latterBoxAnimator.enabled = false;
        FadeAnimation();
        if (miniGameUIPanel != null) miniGameUIPanel.SetActive(false);
        if (changeClothesUIPanel != null) changeClothesUIPanel.SetActive(false);
    }

    /// <summary>
    /// ���͹ڽ��� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ�ϴ� �Լ�
    /// </summary>
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

    /// <summary>
    /// �̴ϰ��� UI�� ������Ʈ�ϴ� �Լ�
    /// </summary>
    public void UpdateMiniGameUI(ESceneType miniGameKey)
    {
        MiniGameDataSO miniGames = GameManager.Instance.GetMiniGameInfo(miniGameKey);

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

    public void SelectClothes(int id)
    {
        clothesController.ChangeClothes(id);
    }
    public void SelectColor(int id)
    {
        clothesController.ChangeColors(id);
    }
    #endregion

}
