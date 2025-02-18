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
    [SerializeField] private GameObject completeText;
    [SerializeField] private Animator completeTextAnim;

    private string[] tmiMessages = {
        "TMI : 와! 자고 싶다.",
        "TMI : 와! 배고프다.",
        "TMI : 이번 팀 프로젝트는 어떨까?",
        "TMI : 난 이 게임을 만들었어요!",
        "TMI : 전 게임을 좋아합니다."
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
        completeText.SetActive(false);
        loadingScreen.SetActive(true);
        progressBar.value = 0;
        tmiText.text = tmiMessages[Random.Range(0, tmiMessages.Length)];

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneType.ToString());
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            progressBar.value = Mathf.Clamp01(operation.progress / 0.9f);

            if (operation.progress >= 0.9f)
            {
                completeText.SetActive(true);
                completeTextAnim.Play("CompleteLoadSceneText");
                while (!Input.anyKeyDown)
                {
                    yield return null;
                }
                operation.allowSceneActivation = true;

                loadingScreen.SetActive(false);
            }
            yield return null;
        }
    }
}
