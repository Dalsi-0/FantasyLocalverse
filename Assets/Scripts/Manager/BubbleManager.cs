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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 말풍선이 있는 객체를 등록하는 함수
    /// </summary>
    public void RegisterBubble(GameObject obj, BubbleAutoResizer bubble)
    {
        if (!bubbleDictionary.ContainsKey(obj))
        {
            bubbleDictionary.Add(obj, bubble);
        }
    }

    /// <summary>
    /// 특정 객체의 말풍선에 텍스트를 출력
    /// </summary>
    public void ShowBubble(GameObject obj, string message, float duration = 2f)
    {
        if (bubbleDictionary.TryGetValue(obj, out BubbleAutoResizer bubble))
        {
            StartCoroutine(ShowAndHideBubble(bubble, message, duration));
        }
    }

    /// <summary>
    /// 일정 시간이 지난 후 말풍선을 숨김
    /// </summary>
    private IEnumerator ShowAndHideBubble(BubbleAutoResizer bubble, string message, float duration)
    {
        bubble.SetBubble(message);
        yield return bubble.StartCoroutine(bubble.TypingRoutine()); 
        yield return new WaitForSeconds(duration); 
        bubble.HideBubble(); 
    }
}
