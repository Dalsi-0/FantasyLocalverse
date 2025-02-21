using TMPro;
using UnityEngine;
using UnityEngine.UI;
using KoreanTyper;
using System.Net.NetworkInformation;
using System.Collections;

public class BubbleAutoResizer : MonoBehaviour
{
    [SerializeField] private Image bubbleImage; // 말풍선 배경
    [SerializeField] private TMP_Text bubbleText; // 말풍선 텍스트
    [SerializeField] private Vector2 maxSize = new Vector2(6f, 6f); // 최대 크기
    [SerializeField] private Vector2 minSize = new Vector2(2f, 1f); // 최소 크기
    [SerializeField] private Vector2 padding = new Vector2(0.5f, 0.5f); // 패딩 (여백)

    private RectTransform textRect;
    private RectTransform bubbleRect;

    private string originText;

    private void Awake()
    {
        textRect = bubbleText.GetComponent<RectTransform>();
        bubbleRect = bubbleImage.GetComponent<RectTransform>();

        bubbleImage.gameObject.SetActive(false);
        bubbleText.gameObject.SetActive(false);
    }

    private void Start()
    {
        BubbleManager.Instance.RegisterBubble(gameObject, this);
    }

    /// <summary>
    /// 최초 메세지 전달
    /// </summary>
    /// <param name="message"></param>
    public void SetBubble(string message)
    {
        bubbleImage.gameObject.SetActive(true);
        bubbleText.gameObject.SetActive(true);
        SetText(message);
    }

    /// <summary>
    /// 말풍선 숨기기
    /// </summary>
    public void HideBubble()
    {
        bubbleText.text = "";
        bubbleImage.gameObject.SetActive(false);
        bubbleText.gameObject.SetActive(false);
    }

    /// <summary>
    /// 텍스트 내용을 변경하고 말풍선 크기를 자동 조절하는 함수
    /// </summary>
    private void SetText(string message)
    {
        bubbleText.SetText(message);
        originText = bubbleText.text;
        ResizeBubble();
    }

    /// <summary>
    /// 텍스트 크기에 맞춰 말풍선 크기 자동 조절
    /// </summary>
    private void ResizeBubble()
    {
        Vector2 textSize = bubbleText.GetPreferredValues(maxSize.x, maxSize.y);
        textSize.x = Mathf.Clamp(textSize.x, minSize.x, maxSize.x);
        textSize.y = Mathf.Clamp(textSize.y, minSize.y, maxSize.y);

        bubbleRect.sizeDelta = (textSize + padding) + new Vector2(2,2);
        textRect.sizeDelta = textSize;

        StartCoroutine(TypingRoutine());
    }

    /// <summary>
    /// 한글 타이핑 효과 코루틴
    /// </summary>
    /// <returns></returns>
    public IEnumerator TypingRoutine()
    {
        int typingLength = originText.GetTypingLength();

        for (int i = 0; i <= typingLength; i++)
        {
            bubbleText.text = originText.Typing(i);
            yield return new WaitForSeconds(0.04f);
        }
    }
}
