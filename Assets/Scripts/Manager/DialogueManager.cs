using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using KoreanTyper;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private TMP_Text speakerText; // 화자 출력 텍스트
    [SerializeField] private TMP_Text dialogueText; // 대사 출력 텍스트
    [SerializeField] private GameObject dialoguePanel; // 대화 UI 패널
    [SerializeField] private float typingSpeed = 0.04f; // 타이핑 속도

    public DialogueRepository repository;

    private Queue<DialogueData> dialogueQueue; // 대사 저장
    private bool inputLock = false; // 키 입력을 막는 변수
    private bool isTyping = false; // 현재 타이핑 중인지 여부
    private Action onDialogueEnd; // 대화 종료 후 실행할 액션
    private DialogueData currentDialogue; // 현재 출력 중인 대사

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            dialogueQueue = new Queue<DialogueData>();
            dialoguePanel.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 대화 시작 
    /// </summary>
    public void StartDialogue(DialogueData[] dialogues, GameObject virtualCamera = null, Action onEndAction = null)
    {
        GameManager.Instance.player.transform.GetComponent<PlayerController>().SetMoveLock(true);
        dialogueQueue.Clear();
        foreach (DialogueData dialogue in dialogues)
        {
            dialogueQueue.Enqueue(dialogue);
        }

        onDialogueEnd = onEndAction;
        dialoguePanel.SetActive(true); 
        
        if (virtualCamera != null)
        {
            virtualCamera.SetActive(true);
        }

        UIManager.Instance.FadeAnimation();
        UIManager.Instance.ActiveOrDisableLetterbox(true);

        inputLock = true; // 입력을 잠시 차단
        StartCoroutine(UnlockInputAfterDelay()); // 0.1초 후 입력 허용
        DisplayNextDialogue();
    }

    private IEnumerator UnlockInputAfterDelay()
    {
        yield return new WaitForSeconds(0.1f); // 0.1초 동안 입력 차단
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
        speakerText.text = currentDialogue.speaker; // 화자 표시

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
            dialogueText.text = currentDialogue.message.Typing(i);
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    private void Update()
    {
        if (dialoguePanel.activeSelf && !inputLock && Input.anyKeyDown)
        {
            if (isTyping)
            {
                // 타이핑 스킵: 전체 문장 바로 출력
                StopAllCoroutines();
                dialogueText.text = currentDialogue.message;
                isTyping = false;
            }
            else
            {
                // 다음 문장 출력
                DisplayNextDialogue();
            }
        }
    }

    /// <summary>
    /// 대화 종료
    /// </summary>
    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        UIManager.Instance.ActiveOrDisableLetterbox(false);
        UIManager.Instance.FadeAnimation();
        dialogueText.text = "";
        speakerText.text = "";
        GameManager.Instance.player.transform.GetComponent<PlayerController>().SetMoveLock(false);
        onDialogueEnd?.Invoke(); // 대화 끝난 후 실행할 기능 호출
    }
}
