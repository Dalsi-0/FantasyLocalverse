using TMPro;
using UnityEngine;
using UnityEngine.UI;
using KoreanTyper;


using System.Net.NetworkInformation;
using System.Collections;

public class BubbleAutoResizer : MonoBehaviour
{
    [SerializeField] private Image bubbleImage; // ��ǳ�� ���
    [SerializeField] private TMP_Text bubbleText; // ��ǳ�� �ؽ�Ʈ
    [SerializeField] private Vector2 maxSize = new Vector2(6f, 6f); // �ִ� ũ��
    [SerializeField] private Vector2 minSize = new Vector2(2f, 1f); // �ּ� ũ��
    [SerializeField] private Vector2 padding = new Vector2(0.5f, 0.5f); // �е� (����)

    private RectTransform textRect;
    private RectTransform bubbleRect;
    private BubbleAutoResizer bubble;

    private string originText;

    private void Awake()
    {
        textRect = bubbleText.GetComponent<RectTransform>();
        bubbleRect = bubbleImage.GetComponent<RectTransform>();
        bubble = GetComponent<BubbleAutoResizer>();

        bubbleImage.gameObject.SetActive(false);
        bubbleText.gameObject.SetActive(false);
    }

    private void Start()
    {
        BubbleManager.Instance.RegisterBubble(gameObject, this);
    }

    public void SetBubble(string message)
    {
        bubbleImage.gameObject.SetActive(true);
        bubbleText.gameObject.SetActive(true);
        bubble.SetText(message);
    }


    /// <summary>
    /// �ؽ�Ʈ ������ �����ϰ� ��ǳ�� ũ�⸦ �ڵ� �����ϴ� �Լ�
    /// </summary>
    void SetText(string message)
    {
        bubbleText.SetText(message);
        originText = bubbleText.text;
        ResizeBubble();
    }

    /// <summary>
    /// �ؽ�Ʈ ũ�⿡ ���� ��ǳ�� ũ�� �ڵ� ����
    /// </summary>
    private void ResizeBubble()
    {
        // �ؽ�Ʈ ũ�� ���
        Vector2 textSize = bubbleText.GetPreferredValues(maxSize.x, maxSize.y);
        textSize.x = Mathf.Clamp(textSize.x, minSize.x, maxSize.x);
        textSize.y = Mathf.Clamp(textSize.y, minSize.y, maxSize.y);

        // ��ǳ�� ũ�� ����
        bubbleRect.sizeDelta = (textSize + padding) + new Vector2(2,2);
        textRect.sizeDelta = textSize;

        StartCoroutine(TypingRoutine());
    }

    /// <summary>
    /// �ѱ� Ÿ���� ȿ�� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator TypingRoutine()
    {
        int typingLength = originText.GetTypingLength();

        for (int i = 0; i <= typingLength; i++)
        {
            bubbleText.text = originText.Typing(i);
            yield return new WaitForSeconds(0.05f);
        }
    }

}
