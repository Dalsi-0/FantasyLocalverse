using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ESceneType
{
    Village,
    MiniGameBrid,
    MiniGameFind,
}

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TMP_Text tmiText;
    [SerializeField] private Button startButton;

    private string[] tmiMessages = {
        "��! �ڰ� �ʹ�.",
        "��! �������.",
        "�̹� �� ������Ʈ�� ���?",
        "�� �� ������ ��������!",
        "�� ������ �����մϴ�."
    };

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

    public void LoadScene(ESceneType sceneType)
    {
        StartCoroutine(LoadSceneAsync(sceneType));
    }

    private IEnumerator LoadSceneAsync(ESceneType sceneType)
    {
        loadingScreen.SetActive(true);
        progressBar.value = 0;
        startButton.gameObject.SetActive(false);
        tmiText.text = tmiMessages[Random.Range(0, tmiMessages.Length)];

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneType.ToString());
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            progressBar.value = Mathf.Clamp01(operation.progress / 0.9f);

            if (operation.progress >= 0.9f)
            {
                startButton.gameObject.SetActive(true);
                startButton.onClick.RemoveAllListeners();
                startButton.onClick.AddListener(() => operation.allowSceneActivation = true);
                yield break;
            }

            yield return null;
        }
    }
}
