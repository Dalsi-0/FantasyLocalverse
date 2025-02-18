using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DialogueData
{
    public string speaker; // 말하는 사람
    public string message; // 대사

    public DialogueData(string speaker, string message)
    {
        this.speaker = speaker;
        this.message = message;
    }
}
public class DialogueRepository : MonoBehaviour
{
    private Dictionary<string, DialogueData[]> dialogues = new Dictionary<string, DialogueData[]>();

    private void Awake()
    {
        dialogues.Add("Object_church", new DialogueData[]
        {
            new DialogueData("안내", "성당안에서 사람 말 소리가 들려온다."),
            new DialogueData("Player", "성당에 들릴 필요는 없다."),
        });

        dialogues.Add("Object_viewPoint", new DialogueData[]
        {
            new DialogueData("안내", "마을 전경이 한 눈에 들어온다."),
        });

        dialogues.Add("EVENT_QUEST_COMPLETE", new DialogueData[]
        {
            new DialogueData("System", "축하합니다! 퀘스트를 완료하셨습니다!"),
            new DialogueData("System", "보상을 받으세요!")
        });
    }

    public DialogueData[] GetDialogue(string key)
    {
        if (dialogues.TryGetValue(key, out DialogueData[] dialogueLines))
        {
            return dialogueLines;
        }
        return new DialogueData[] { new DialogueData("System", "대사가 없습니다.") };
    }
}
