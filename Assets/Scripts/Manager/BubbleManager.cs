using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public static BubbleManager Instance { get; private set; } // �̱��� �ν��Ͻ�

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
            bubble.SetBubble(message);
            StartCoroutine(HideBubbleAfterDelay(obj, duration));
        }
    }

    /// <summary>
    /// ���� �ð��� ���� �� ��ǳ���� ����
    /// </summary>
    private IEnumerator HideBubbleAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (bubbleDictionary.TryGetValue(obj, out BubbleAutoResizer bubble))
        {
            bubble.gameObject.SetActive(false);
        }
    }
}
