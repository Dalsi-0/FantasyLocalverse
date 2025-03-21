using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static DialogueDataSO;

public enum EDialogueKey
{
    Object_church,
    Object_viewPoint,
    MiniGame_Brid,
    NPC_merchant,
    MiniGame_Find,
    MiniGame_FakeItem,
    MiniGame_RealItem,
}

public class DialogueRepository : MonoBehaviour
{
    [SerializeField] private List<DialogueDataSO> dialogueDataList;

    private Dictionary<EDialogueKey, List<DialogueLine>> dialogueDictionary = new Dictionary<EDialogueKey, List<DialogueLine>>();

    public TMP_Text speakerText; // 화자 출력 텍스트
    public TMP_Text dialogueText; // 대사 출력 텍스트
    public GameObject dialoguePanel; // 대화 UI 패널

    private void Awake()
    {
        foreach (var dialogueData in dialogueDataList)
        {
            if (!dialogueDictionary.ContainsKey(dialogueData.dialogueKey))
            {
                dialogueDictionary.Add(dialogueData.dialogueKey, dialogueData.dialogueLines);
            }
        }
    }

    public List<DialogueLine> GetDialogue(EDialogueKey key)
    {
        if (dialogueDictionary.TryGetValue(key, out List<DialogueLine> dialogueLines))
        {
            return dialogueLines;
        }
        return new List<DialogueLine>();
    }
}
