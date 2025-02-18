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

    [SerializeField] private GameObject[] managers;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TMP_Text tmiText;
    [SerializeField] private GameObject completeText;
    [SerializeField] private Animator completeTextAnim;
    private Vector3 originPlayerPosition;

    private float fakeProgress = 0f; 

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
        if (SceneManager.GetActiveScene().name == ESceneType.Village.ToString())
        {
            SavePlayerPosition();
        }

        StartCoroutine(LoadSceneAsync(sceneType));
    }

    private IEnumerator LoadSceneAsync(ESceneType sceneType)
    {
        float minLoadTime = 2.0f;
        float elapsedTime = 0f; 

        loadingScreen.SetActive(true);
        progressBar.value = 0;
        fakeProgress = 0;
        completeText.SetActive(false);
        tmiText.text = tmiMessages[Random.Range(0, tmiMessages.Length)];

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneType.ToString());
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            elapsedTime += Time.deltaTime;

            float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);
            fakeProgress = Mathf.Lerp(fakeProgress, targetProgress, Time.deltaTime * 3f);
            progressBar.value = fakeProgress;

            if (fakeProgress >= 0.9f && elapsedTime >= minLoadTime)
            {
                completeText.SetActive(true);

                while (!Input.anyKeyDown)
                {
                    yield return null;
                }

                operation.allowSceneActivation = true;

                loadingScreen.SetActive(false);

                // 씬이 완전히 로드된 다음 프레임에 RestorePlayerPosition 실행
                yield return new WaitForSeconds(0.1f);

                if (sceneType == ESceneType.Village)
                {
                    RestorePlayerPosition();
                }
            }

            yield return null;
        }
    }
    private void SavePlayerPosition()
    {
        SetManagersActive(false);
        originPlayerPosition = GameManager.Instance.player.transform.position;
    }

    private void RestorePlayerPosition()
    {
        SetManagersActive(true);
        Camera.main.transform.position = originPlayerPosition;
        GameManager.Instance.player.transform.position = originPlayerPosition;
    }

    private void SetManagersActive(bool state)
    {
        for (int i = 0; i < managers.Length; i++)
        {
          //  managers[i].SetActive(state);
        }
    }
}
