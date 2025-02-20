using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using KoreanTyper;
using static DialogueDataSO;
using UnityEngine.SceneManagement;

public class DialogueManager : BaseManager
{
    public static DialogueManager Instance { get; private set; }

    private DialogueRepository repository;
    private Queue<DialogueLine> dialogueQueue; // 대사 저장
    private bool inputLock = false; // 키 입력을 막는 변수
    private bool isTyping = false; // 현재 타이핑 중인지 여부
    private Action onDialogueEnd; // 대화 종료 후 실행할 액션
    private DialogueLine currentDialogue; // 현재 출력 중인 대사
    private float typingSpeed = 0.04f; // 타이핑 속도

    protected override void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            dialogueQueue = new Queue<DialogueLine>();
            base.Awake();

            FindRepository();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindRepository();
    }

    protected override void FindRepository()
    {
        repository = FindObjectOfType<DialogueRepository>();
    }

    public List<DialogueLine> GetDialogue(EDialogueKey key)
    {
        return repository.GetDialogue(key);
    }

    /// <summary>
    /// 대화 시작 
    /// </summary>
    public void StartDialogue(List<DialogueLine> dialogues, GameObject virtualCamera = null, Action onEndAction = null)
    {
        GameManager.Instance.PlayerController.SetMoveLock(true);
        dialogueQueue.Clear();
        foreach (var dialogue in dialogues)
        {
            dialogueQueue.Enqueue(dialogue);
        }
        onDialogueEnd = onEndAction;
        repository.dialoguePanel.SetActive(true);

        virtualCamera?.SetActive(true);
        UIManager.Instance.FadeAnimation();
        UIManager.Instance.ActiveOrDisableLetterbox(true);

        inputLock = true; // 입력을 잠시 차단
        StartCoroutine(UnlockInputAfterDelay()); 
        DisplayNextDialogue();
    }

    private IEnumerator UnlockInputAfterDelay()
    {
        yield return new WaitForSeconds(0.1f); 
        inputLock = false;
    }

    /// <summary>
    /// 다음 대사 출력
    /// </summary>
    private void DisplayNextDialogue()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentDialogue = dialogueQueue.Dequeue();
        repository.speakerText.text = currentDialogue.speaker; 
        StartCoroutine(TypingRoutine());
    }

    /// <summary>
    /// 한글 타이핑 효과 코루틴
    /// </summary>
    private IEnumerator TypingRoutine()
    {
        int typingLength = currentDialogue.message.GetTypingLength();

        yield return new WaitForSeconds(0.01f);
        isTyping = true;
        for (int i = 0; i <= typingLength; i++)
        {
            repository.dialogueText.text = currentDialogue.message.Typing(i);
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    private void Update()
    {
        if (repository == null) return;
        if (repository.dialoguePanel.activeSelf && !inputLock && Input.anyKeyDown)
        {
            if (isTyping)
            {
                // 타이핑 스킵
                StopAllCoroutines();
                repository.dialogueText.text = currentDialogue.message;
                isTyping = false;
            }
            else
            {
                DisplayNextDialogue();
            }
        }
    }

    /// <summary>
    /// 대화 종료
    /// </summary>
    private void EndDialogue()
    {
        repository.dialoguePanel.SetActive(false);
        UIManager.Instance.ActiveOrDisableLetterbox(false);
        UIManager.Instance.FadeAnimation();
        repository.dialogueText.text = "";
        repository.speakerText.text = "";
        GameManager.Instance.PlayerController.SetMoveLock(false);
        onDialogueEnd?.Invoke(); // 대화 끝난 후 실행할 기능 호출
    }
}
