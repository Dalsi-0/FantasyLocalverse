using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public static BubbleManager Instance { get; private set; }

    private Dictionary<GameObject, BubbleAutoResizer> bubbleDictionary = new Dictionary<GameObject, BubbleAutoResizer>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ��ǳ���� �ִ� ��ü�� ����ϴ� �Լ�
    /// </summary>
    public void RegisterBubble(GameObject obj, BubbleAutoResizer bubble)
    {
        if (!bubbleDictionary.ContainsKey(obj))
        {
            bubbleDictionary.Add(obj, bubble);
        }
    }

    /// <summary>
    /// Ư�� ��ü�� ��ǳ���� �ؽ�Ʈ�� ���
    /// </summary>
    public void ShowBubble(GameObject obj, string message, float duration = 2f)
    {
        if (bubbleDictionary.TryGetValue(obj, out BubbleAutoResizer bubble))
        {
            StartCoroutine(ShowAndHideBubble(bubble, message, duration));
        }
    }

    /// <summary>
    /// ���� �ð��� ���� �� ��ǳ���� ����
    /// </summary>
    private IEnumerator ShowAndHideBubble(BubbleAutoResizer bubble, string message, float duration)
    {
        bubble.SetBubble(message);
        yield return bubble.StartCoroutine(bubble.TypingRoutine()); // Ÿ���� ȿ���� ���� ������ ���
        yield return new WaitForSeconds(duration); // �߰��� 2�� ���
        bubble.HideBubble(); // ��ǳ�� �����
    }
}
