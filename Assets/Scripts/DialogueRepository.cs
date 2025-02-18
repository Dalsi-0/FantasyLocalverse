using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DialogueData
{
    public string speaker; // ���ϴ� ���
    public string message; // ���

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
            new DialogueData("�ȳ�", "����ȿ��� ��� �� �Ҹ��� ����´�."),
            new DialogueData("Player", "���翡 �鸱 �ʿ�� ����."),
        });

        dialogues.Add("Object_viewPoint", new DialogueData[]
        {
            new DialogueData("�ȳ�", "���� ������ �� ���� ���´�."),
        });

        dialogues.Add("MiniGame_Brid", new DialogueData[]
        {
            new DialogueData("�ȳ�", "���� �������� ������ ��� �峲�� �����̴�."),
            new DialogueData("�ȳ�", "���� �׾ƿ÷��� ���� ���̷� ���� ���� ���ƴٴѴ�."),
        });

        dialogues.Add("NPC_merchant", new DialogueData[]
        {
            new DialogueData("���� ����", "�������. ���ִ� ���� �����ϴ�."),
            new DialogueData("Player", "���⺸�� ��δ�."),
        });

        dialogues.Add("NPC_quest", new DialogueData[]
        {
            new DialogueData("�ҽ��� �ֹ�", "�ű�, �� �� �����ֿ�.."),
            new DialogueData("Player", "���� ���̽Ű���?"),
            new DialogueData("�ҽ��� �ֹ�", "�Ҿ���� �� ���� �� ã����..\n������ �������� �ʰ� ì������"),
        });

        dialogues.Add("EVENT_QUEST_COMPLETE", new DialogueData[]
        {
            new DialogueData("System", "�����մϴ�! ����Ʈ�� �Ϸ��ϼ̽��ϴ�!"),
            new DialogueData("System", "������ ��������!")
        });
    }

    public DialogueData[] GetDialogue(string key)
    {
        if (dialogues.TryGetValue(key, out DialogueData[] dialogueLines))
        {
            return dialogueLines;
        }
        return new DialogueData[] { new DialogueData("System", "��簡 �����ϴ�.") };
    }
}
