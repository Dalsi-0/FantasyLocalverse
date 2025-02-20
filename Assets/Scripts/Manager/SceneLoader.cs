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
        "TMI : 오랜만에 유니티 재밌었습니다~",
        "TMI : 매일같이 야근한 결과입니다!",
        "TMI : 리팩토링 아직 다 못했어요 ㅠ"
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

    /// <summary>
    /// 지정된 씬을 로드하는 함수
    /// </summary>
    public void LoadScene(ESceneType sceneType)
    {
        if (SceneManager.GetActiveScene().name == ESceneType.Village.ToString())
        {
            SavePlayerPosition();
        }

        StartCoroutine(LoadSceneAsync(sceneType));
    }

    /// <summary>
    /// 씬을 비동기로 로드하는 코루틴
    /// </summary>
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

                // 씬이 완전히 로드된 다음에 캐릭터 이동
                yield return new WaitForSeconds(0.1f);

                if (sceneType == ESceneType.Village)
                {
                    RestorePlayerPosition();
                }
            }

            yield return null;
        }
    }

    /// <summary>
    /// 현재 플레이어 위치를 저장하는 함수
    /// </summary>
    private void SavePlayerPosition()
    {
        originPlayerPosition = GameManager.Instance.player.transform.position;
    }

    /// <summary>
    /// 저장된 위치로 플레이어를 복원하는 함수
    /// </summary>
    private void RestorePlayerPosition()
    {
        Camera.main.transform.position = originPlayerPosition;
        GameManager.Instance.player.transform.position = originPlayerPosition;
    }
}
