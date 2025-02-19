using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DialogueData", menuName = "Dialogue/Create Dialogue Data")]
public class DialogueDataSO : ScriptableObject
{
    public EDialogueKey dialogueKey;
    public List<DialogueLine> dialogueLines;

    [System.Serializable]
    public struct DialogueLine
    {
        public string speaker;  // ���ϴ� ���
        [TextArea] public string message;  // ���
    }
}