using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseManager : MonoBehaviour
{
    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    protected virtual void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    protected virtual void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    protected abstract void OnSceneLoaded(Scene scene, LoadSceneMode mode);
    protected virtual void FindRepository() { }
}
