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
    private Queue<DialogueLine> dialogueQueue; // ��� ����
    private bool inputLock = false; // Ű �Է��� ���� ����
    private bool isTyping = false; // ���� Ÿ���� ������ ����
    private Action onDialogueEnd; // ��ȭ ���� �� ������ �׼�
    private DialogueLine currentDialogue; // ���� ��� ���� ���
    private float typingSpeed = 0.04f; // Ÿ���� �ӵ�

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
    /// ��ȭ ���� 
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

        inputLock = true; // �Է��� ��� ����
        StartCoroutine(UnlockInputAfterDelay()); 
        DisplayNextDialogue();
    }

    private IEnumerator UnlockInputAfterDelay()
    {
        yield return new WaitForSeconds(0.1f); 
        inputLock = false;
    }

    /// <summary>
    /// ���� ��� ���
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
    /// �ѱ� Ÿ���� ȿ�� �ڷ�ƾ
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
                // Ÿ���� ��ŵ
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
    /// ��ȭ ����
    /// </summary>
    private void EndDialogue()
    {
        repository.dialoguePanel.SetActive(false);
        UIManager.Instance.ActiveOrDisableLetterbox(false);
        UIManager.Instance.FadeAnimation();
        repository.dialogueText.text = "";
        repository.speakerText.text = "";
        GameManager.Instance.PlayerController.SetMoveLock(false);
        onDialogueEnd?.Invoke(); // ��ȭ ���� �� ������ ��� ȣ��
    }
}
