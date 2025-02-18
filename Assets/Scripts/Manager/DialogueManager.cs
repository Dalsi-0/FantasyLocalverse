using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using KoreanTyper;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private TMP_Text speakerText; // ȭ�� ��� �ؽ�Ʈ
    [SerializeField] private TMP_Text dialogueText; // ��� ��� �ؽ�Ʈ
    [SerializeField] private GameObject dialoguePanel; // ��ȭ UI �г�
    [SerializeField] private float typingSpeed = 0.04f; // Ÿ���� �ӵ�

    public DialogueRepository repository;

    private Queue<DialogueData> dialogueQueue; // ��� ����
    private bool inputLock = false; // Ű �Է��� ���� ����
    private bool isTyping = false; // ���� Ÿ���� ������ ����
    private Action onDialogueEnd; // ��ȭ ���� �� ������ �׼�
    private DialogueData currentDialogue; // ���� ��� ���� ���

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            dialogueQueue = new Queue<DialogueData>();
            dialoguePanel.SetActive(false);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ��ȭ ���� 
    /// </summary>
    public void StartDialogue(DialogueData[] dialogues, Action onEndAction = null)
    {
        GameManager.Instance.player.transform.GetComponent<PlayerController>().SetMoveLock(true);
        dialogueQueue.Clear();
        foreach (DialogueData dialogue in dialogues)
        {
            dialogueQueue.Enqueue(dialogue);
        }

        onDialogueEnd = onEndAction;
        dialoguePanel.SetActive(true);

        inputLock = true; // �Է��� ��� ����
        StartCoroutine(UnlockInputAfterDelay()); // 0.1�� �� �Է� ���
        DisplayNextDialogue();
    }

    private IEnumerator UnlockInputAfterDelay()
    {
        yield return new WaitForSeconds(0.1f); // 0.1�� ���� �Է� ����
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
        speakerText.text = currentDialogue.speaker; // ȭ�� ǥ��

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
                // Ÿ���� ��ŵ: ��ü ���� �ٷ� ���
                StopAllCoroutines();
                dialogueText.text = currentDialogue.message;
                isTyping = false;
            }
            else
            {
                // ���� ���� ���
                DisplayNextDialogue();
            }
        }
    }

    /// <summary>
    /// ��ȭ ����
    /// </summary>
    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        speakerText.text = "";
        GameManager.Instance.player.transform.GetComponent<PlayerController>().SetMoveLock(false);
        onDialogueEnd?.Invoke(); // ��ȭ ���� �� ������ ��� ȣ��
    }
}
